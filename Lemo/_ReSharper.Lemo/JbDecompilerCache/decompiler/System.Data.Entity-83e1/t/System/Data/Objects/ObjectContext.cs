// Type: System.Data.Objects.ObjectContext
// Assembly: System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Data.Entity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Internal.Materialization;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Objects.ELinq;
using System.Data.Objects.Internal;
using System.Data.Query.InternalTrees;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Transactions;

namespace System.Data.Objects
{
  public class ObjectContext : IDisposable
  {
    private IEntityAdapter _adapter;
    private EntityConnection _connection;
    private readonly MetadataWorkspace _workspace;
    private ObjectStateManager _cache;
    private ClrPerspective _perspective;
    private readonly bool _createdConnection;
    private bool _openedConnection;
    private int _connectionRequestCount;
    private int? _queryTimeout;
    private Transaction _lastTransaction;
    private bool _disallowSettingDefaultContainerName;
    private EventHandler _onSavingChanges;
    private ObjectMaterializedEventHandler _onObjectMaterialized;
    private ObjectQueryProvider _queryProvider;
    private readonly ObjectContextOptions _options;
    private readonly string s_UseLegacyPreserveChangesBehavior;

    public DbConnection Connection
    {
      get
      {
        if (this._connection == null)
          throw EntityUtil.ObjectContextDisposed();
        else
          return (DbConnection) this._connection;
      }
    }

    public string DefaultContainerName
    {
      get
      {
        EntityContainer defaultContainer = this.Perspective.GetDefaultContainer();
        if (defaultContainer == null)
          return string.Empty;
        else
          return defaultContainer.Name;
      }
      set
      {
        if (this._disallowSettingDefaultContainerName)
          throw EntityUtil.CannotSetDefaultContainerName();
        this.Perspective.SetDefaultContainer(value);
      }
    }

    [CLSCompliant(false)]
    public MetadataWorkspace MetadataWorkspace
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._workspace;
      }
    }

    public ObjectStateManager ObjectStateManager
    {
      get
      {
        if (this._cache == null)
          this._cache = new ObjectStateManager(this._workspace);
        return this._cache;
      }
    }

    internal ClrPerspective Perspective
    {
      get
      {
        if (this._perspective == null)
          this._perspective = new ClrPerspective(this._workspace);
        return this._perspective;
      }
    }

    public int? CommandTimeout
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._queryTimeout;
      }
      set
      {
        if (value.HasValue)
        {
          int? nullable = value;
          if ((nullable.GetValueOrDefault() >= 0 ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
            throw EntityUtil.InvalidCommandTimeout("value");
        }
        this._queryTimeout = value;
      }
    }

    protected internal IQueryProvider QueryProvider
    {
      get
      {
        if (this._queryProvider == null)
          this._queryProvider = new ObjectQueryProvider(this);
        return (IQueryProvider) this._queryProvider;
      }
    }

    internal bool InMaterialization { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    public ObjectContextOptions ContextOptions
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this._options;
      }
    }

    internal bool OnMaterializedHasHandlers
    {
      get
      {
        if (this._onObjectMaterialized != null)
          return this._onObjectMaterialized.GetInvocationList().Length != 0;
        else
          return false;
      }
    }

    internal CollectionColumnMap ColumnMapBuilder { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    public event EventHandler SavingChanges
    {
      add
      {
        this._onSavingChanges += value;
      }
      remove
      {
        this._onSavingChanges -= value;
      }
    }

    public event ObjectMaterializedEventHandler ObjectMaterialized
    {
      add
      {
        this._onObjectMaterialized += value;
      }
      remove
      {
        this._onObjectMaterialized -= value;
      }
    }

    public ObjectContext(EntityConnection connection)
      : this(EntityUtil.CheckArgumentNull<EntityConnection>(connection, "connection"), true)
    {
    }

    public ObjectContext(string connectionString)
      : this(ObjectContext.CreateEntityConnection(connectionString), false)
    {
      this._createdConnection = true;
    }

    protected ObjectContext(string connectionString, string defaultContainerName)
      : this(connectionString)
    {
      this.DefaultContainerName = defaultContainerName;
      if (string.IsNullOrEmpty(defaultContainerName))
        return;
      this._disallowSettingDefaultContainerName = true;
    }

    protected ObjectContext(EntityConnection connection, string defaultContainerName)
      : this(connection)
    {
      this.DefaultContainerName = defaultContainerName;
      if (string.IsNullOrEmpty(defaultContainerName))
        return;
      this._disallowSettingDefaultContainerName = true;
    }

    private ObjectContext(EntityConnection connection, bool isConnectionConstructor)
    {
      this._connection = connection;
      this._connection.StateChange += new StateChangeEventHandler(this.ConnectionStateChange);
      string connectionString = connection.ConnectionString;
      if (connectionString != null)
      {
        if (connectionString.Trim().Length != 0)
        {
          try
          {
            this._workspace = this.RetrieveMetadataWorkspaceFromConnection();
          }
          catch (InvalidOperationException ex)
          {
            throw EntityUtil.InvalidConnection(isConnectionConstructor, (Exception) ex);
          }
          if (this._workspace != null)
          {
            if (!this._workspace.IsItemCollectionAlreadyRegistered(DataSpace.OSpace))
              this._workspace.RegisterItemCollection((ItemCollection) new ObjectItemCollection());
            this._workspace.GetItemCollection(DataSpace.OCSpace);
          }
          string str = ConfigurationManager.AppSettings[this.s_UseLegacyPreserveChangesBehavior];
          bool result = false;
          if (!bool.TryParse(str, out result))
            return;
          this.ContextOptions.UseLegacyPreserveChangesBehavior = result;
          return;
        }
      }
      throw EntityUtil.InvalidConnection(isConnectionConstructor, (Exception) null);
    }

    internal void OnObjectMaterialized(object entity)
    {
      if (this._onObjectMaterialized == null)
        return;
      this._onObjectMaterialized((object) this, new ObjectMaterializedEventArgs(entity));
    }

    public void AcceptAllChanges()
    {
      if (this.ObjectStateManager.SomeEntryWithConceptualNullExists())
        throw new InvalidOperationException(System.Data.Entity.Strings.ObjectContext_CommitWithConceptualNull);
      foreach (ObjectStateEntry objectStateEntry in this.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted))
        objectStateEntry.AcceptChanges();
      foreach (ObjectStateEntry objectStateEntry in this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
        objectStateEntry.AcceptChanges();
    }

    public void AddObject(string entitySetName, object entity)
    {
      EntityUtil.CheckArgumentNull<object>(entity, "entity");
      EntityEntry existingEntry;
      IEntityWrapper wrappedEntity = EntityWrapperFactory.WrapEntityUsingContextGettingEntry(entity, this, out existingEntry);
      if (existingEntry == null)
        this.MetadataWorkspace.ImplicitLoadAssemblyForType(wrappedEntity.IdentityType, (Assembly) null);
      EntitySet entitySet;
      bool isNoOperation;
      this.VerifyRootForAdd(false, entitySetName, wrappedEntity, existingEntry, out entitySet, out isNoOperation);
      if (isNoOperation)
        return;
      System.Data.Objects.Internal.TransactionManager transactionManager = this.ObjectStateManager.TransactionManager;
      transactionManager.BeginAddTracking();
      try
      {
        RelationshipManager relationshipManager = wrappedEntity.RelationshipManager;
        bool flag = true;
        try
        {
          this.AddSingleObject(entitySet, wrappedEntity, "entity");
          flag = false;
        }
        finally
        {
          if (flag && wrappedEntity.Context == this)
          {
            EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
            if (entityEntry != null && entityEntry.EntityKey.IsTemporary)
            {
              relationshipManager.NodeVisited = true;
              RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(wrappedEntity);
              RelatedEnd.RemoveEntityFromObjectStateManager(wrappedEntity);
            }
          }
        }
        relationshipManager.AddRelatedEntitiesToObjectStateManager(false);
      }
      finally
      {
        transactionManager.EndAddTracking();
      }
    }

    internal void AddSingleObject(EntitySet entitySet, IEntityWrapper wrappedEntity, string argumentName)
    {
      EntityKey entityKeyFromEntity = wrappedEntity.GetEntityKeyFromEntity();
      if (entityKeyFromEntity != null)
      {
        EntityUtil.ValidateEntitySetInKey(entityKeyFromEntity, entitySet);
        entityKeyFromEntity.ValidateEntityKey(this._workspace, entitySet);
      }
      this.VerifyContextForAddOrAttach(wrappedEntity);
      wrappedEntity.Context = this;
      EntityEntry entityEntry = this.ObjectStateManager.AddEntry(wrappedEntity, (EntityKey) null, entitySet, argumentName, true);
      this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
      wrappedEntity.AttachContext(this, entitySet, MergeOption.AppendOnly);
      entityEntry.FixupFKValuesFromNonAddedReferences();
      this._cache.FixupReferencesByForeignKeys(entityEntry, false);
      wrappedEntity.TakeSnapshotOfRelationships(entityEntry);
    }

    public void LoadProperty(object entity, string navigationProperty)
    {
      this.WrapEntityAndCheckContext(entity, "property").RelationshipManager.GetRelatedEnd(navigationProperty, false).Load();
    }

    public void LoadProperty(object entity, string navigationProperty, MergeOption mergeOption)
    {
      this.WrapEntityAndCheckContext(entity, "property").RelationshipManager.GetRelatedEnd(navigationProperty, false).Load(mergeOption);
    }

    public void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector)
    {
      bool removedConvert;
      string navigationProperty = ObjectContext.ParsePropertySelectorExpression<TEntity>(selector, out removedConvert);
      this.WrapEntityAndCheckContext((object) entity, "property").RelationshipManager.GetRelatedEnd(navigationProperty, removedConvert).Load();
    }

    public void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector, MergeOption mergeOption)
    {
      bool removedConvert;
      string navigationProperty = ObjectContext.ParsePropertySelectorExpression<TEntity>(selector, out removedConvert);
      this.WrapEntityAndCheckContext((object) entity, "property").RelationshipManager.GetRelatedEnd(navigationProperty, removedConvert).Load(mergeOption);
    }

    private IEntityWrapper WrapEntityAndCheckContext(object entity, string refType)
    {
      IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContext(entity, this);
      if (entityWrapper.Context == null)
        throw new InvalidOperationException(System.Data.Entity.Strings.ObjectContext_CannotExplicitlyLoadDetachedRelationships((object) refType));
      if (entityWrapper.Context != this)
        throw new InvalidOperationException(System.Data.Entity.Strings.ObjectContext_CannotLoadReferencesUsingDifferentContext((object) refType));
      else
        return entityWrapper;
    }

    internal static string ParsePropertySelectorExpression<TEntity>(Expression<Func<TEntity, object>> selector, out bool removedConvert)
    {
      EntityUtil.CheckArgumentNull<Expression<Func<TEntity, object>>>(selector, "selector");
      removedConvert = false;
      Expression expression;
      for (expression = selector.Body; expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked; expression = ((UnaryExpression) expression).Operand)
        removedConvert = true;
      MemberExpression memberExpression = expression as MemberExpression;
      if (memberExpression == null || !memberExpression.Member.DeclaringType.IsAssignableFrom(typeof (TEntity)) || memberExpression.Expression.NodeType != ExpressionType.Parameter)
        throw new ArgumentException(System.Data.Entity.Strings.ObjectContext_SelectorExpressionMustBeMemberAccess);
      else
        return memberExpression.Member.Name;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Use ApplyCurrentValues instead")]
    public void ApplyPropertyChanges(string entitySetName, object changed)
    {
      EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
      EntityUtil.CheckArgumentNull<object>(changed, "changed");
      this.ApplyCurrentValues<object>(entitySetName, changed);
    }

    public TEntity ApplyCurrentValues<TEntity>(string entitySetName, TEntity currentEntity) where TEntity : class
    {
      EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
      EntityUtil.CheckArgumentNull<TEntity>(currentEntity, "currentEntity");
      IEntityWrapper wrappedCurrentEntity = EntityWrapperFactory.WrapEntityUsingContext((object) currentEntity, this);
      this.MetadataWorkspace.ImplicitLoadAssemblyForType(wrappedCurrentEntity.IdentityType, (Assembly) null);
      EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
      EntityKey entityKey = wrappedCurrentEntity.EntityKey;
      if (entityKey != null)
      {
        EntityUtil.ValidateEntitySetInKey(entityKey, entitySetFromName, "entitySetName");
        entityKey.ValidateEntityKey(this._workspace, entitySetFromName);
      }
      else
        entityKey = this.ObjectStateManager.CreateEntityKey(entitySetFromName, (object) currentEntity);
      EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
      if (entityEntry == null || entityEntry.IsKeyEntry)
        throw EntityUtil.EntityNotTracked();
      entityEntry.ApplyCurrentValuesInternal(wrappedCurrentEntity);
      return (TEntity) entityEntry.Entity;
    }

    public TEntity ApplyOriginalValues<TEntity>(string entitySetName, TEntity originalEntity) where TEntity : class
    {
      EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
      EntityUtil.CheckArgumentNull<TEntity>(originalEntity, "originalEntity");
      IEntityWrapper entityWrapper = EntityWrapperFactory.WrapEntityUsingContext((object) originalEntity, this);
      this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityWrapper.IdentityType, (Assembly) null);
      EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
      EntityKey entityKey = entityWrapper.EntityKey;
      if (entityKey != null)
      {
        EntityUtil.ValidateEntitySetInKey(entityKey, entitySetFromName, "entitySetName");
        entityKey.ValidateEntityKey(this._workspace, entitySetFromName);
      }
      else
        entityKey = this.ObjectStateManager.CreateEntityKey(entitySetFromName, (object) originalEntity);
      EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entityKey);
      if (entityEntry == null || entityEntry.IsKeyEntry)
        throw EntityUtil.EntityNotTrackedOrHasTempKey();
      if (entityEntry.State != EntityState.Modified && entityEntry.State != EntityState.Unchanged && entityEntry.State != EntityState.Deleted)
        throw EntityUtil.EntityMustBeUnchangedOrModifiedOrDeleted(entityEntry.State);
      if (entityEntry.WrappedEntity.IdentityType != entityWrapper.IdentityType)
        throw EntityUtil.EntitiesHaveDifferentType(entityEntry.Entity.GetType().FullName, originalEntity.GetType().FullName);
      entityEntry.CompareKeyProperties((object) originalEntity);
      entityEntry.UpdateOriginalValues(entityWrapper.Entity);
      return (TEntity) entityEntry.Entity;
    }

    public void AttachTo(string entitySetName, object entity)
    {
      EntityUtil.CheckArgumentNull<object>(entity, "entity");
      EntityEntry existingEntry;
      IEntityWrapper wrappedEntity = EntityWrapperFactory.WrapEntityUsingContextGettingEntry(entity, this, out existingEntry);
      if (existingEntry == null)
        this.MetadataWorkspace.ImplicitLoadAssemblyForType(wrappedEntity.IdentityType, (Assembly) null);
      EntitySet entitySet;
      bool isNoOperation;
      this.VerifyRootForAdd(true, entitySetName, wrappedEntity, existingEntry, out entitySet, out isNoOperation);
      if (isNoOperation)
        return;
      System.Data.Objects.Internal.TransactionManager transactionManager = this.ObjectStateManager.TransactionManager;
      transactionManager.BeginAttachTracking();
      try
      {
        this.ObjectStateManager.TransactionManager.OriginalMergeOption = new MergeOption?(wrappedEntity.MergeOption);
        RelationshipManager relationshipManager = wrappedEntity.RelationshipManager;
        bool flag = true;
        try
        {
          this.AttachSingleObject(wrappedEntity, entitySet, "entity");
          flag = false;
        }
        finally
        {
          if (flag && wrappedEntity.Context == this)
          {
            relationshipManager.NodeVisited = true;
            RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(wrappedEntity);
            RelatedEnd.RemoveEntityFromObjectStateManager(wrappedEntity);
          }
        }
        relationshipManager.AddRelatedEntitiesToObjectStateManager(true);
      }
      finally
      {
        transactionManager.EndAttachTracking();
      }
    }

    public void Attach(IEntityWithKey entity)
    {
      EntityUtil.CheckArgumentNull<IEntityWithKey>(entity, "entity");
      if (entity.EntityKey == null)
        throw EntityUtil.CannotAttachEntityWithoutKey();
      this.AttachTo((string) null, (object) entity);
    }

    internal void AttachSingleObject(IEntityWrapper wrappedEntity, EntitySet entitySet, string argumentName)
    {
      RelationshipManager relationshipManager = wrappedEntity.RelationshipManager;
      EntityKey entityKey = wrappedEntity.GetEntityKeyFromEntity();
      if (entityKey != null)
      {
        EntityUtil.ValidateEntitySetInKey(entityKey, entitySet);
        entityKey.ValidateEntityKey(this._workspace, entitySet);
      }
      else
        entityKey = this.ObjectStateManager.CreateEntityKey(entitySet, wrappedEntity.Entity);
      if (entityKey.IsTemporary)
        throw EntityUtil.CannotAttachEntityWithTemporaryKey();
      if (wrappedEntity.EntityKey != entityKey)
        wrappedEntity.EntityKey = entityKey;
      EntityEntry entityEntry1 = this.ObjectStateManager.FindEntityEntry(entityKey);
      if (entityEntry1 != null)
      {
        if (!entityEntry1.IsKeyEntry)
          throw EntityUtil.ObjectStateManagerContainsThisEntityKey();
        this.ObjectStateManager.PromoteKeyEntryInitialization(this, entityEntry1, wrappedEntity, (IExtendedDataRecord) null, false);
        this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
        wrappedEntity.TakeSnapshotOfRelationships(entityEntry1);
        this.ObjectStateManager.PromoteKeyEntry(entityEntry1, wrappedEntity, (IExtendedDataRecord) null, false, false, true, "Attach");
        this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry1, false);
        relationshipManager.CheckReferentialConstraintProperties(entityEntry1);
      }
      else
      {
        this.VerifyContextForAddOrAttach(wrappedEntity);
        wrappedEntity.Context = this;
        EntityEntry entityEntry2 = this.ObjectStateManager.AttachEntry(entityKey, wrappedEntity, entitySet, argumentName);
        this.ObjectStateManager.TransactionManager.ProcessedEntities.Add(wrappedEntity);
        wrappedEntity.AttachContext(this, entitySet, MergeOption.AppendOnly);
        this.ObjectStateManager.FixupReferencesByForeignKeys(entityEntry2, false);
        wrappedEntity.TakeSnapshotOfRelationships(entityEntry2);
        relationshipManager.CheckReferentialConstraintProperties(entityEntry2);
      }
    }

    public EntityKey CreateEntityKey(string entitySetName, object entity)
    {
      EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
      EntityUtil.CheckArgumentNull<object>(entity, "entity");
      this.MetadataWorkspace.ImplicitLoadAssemblyForType(EntityUtil.GetEntityIdentityType(entity.GetType()), (Assembly) null);
      return this.ObjectStateManager.CreateEntityKey(this.GetEntitySetFromName(entitySetName), entity);
    }

    internal EntitySet GetEntitySetFromName(string entitySetName)
    {
      string entityset;
      string container;
      ObjectContext.GetEntitySetName(entitySetName, "entitySetName", this, out entityset, out container);
      return this.GetEntitySet(entityset, container);
    }

    public ObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class
    {
      return new ObjectSet<TEntity>(this.GetEntitySetForType(typeof (TEntity), "TEntity"), this);
    }

    private EntitySet GetEntitySetForType(Type entityCLRType, string exceptionParameterName)
    {
      EntitySet entitySet = (EntitySet) null;
      EntityContainer defaultContainer = this.Perspective.GetDefaultContainer();
      if (defaultContainer == null)
      {
        foreach (EntityContainer container in this.MetadataWorkspace.GetItems<EntityContainer>(DataSpace.CSpace))
        {
          EntitySet setFromContainer = this.GetEntitySetFromContainer(container, entityCLRType, exceptionParameterName);
          if (setFromContainer != null)
          {
            if (entitySet != null)
              throw EntityUtil.MultipleEntitySetsFoundInAllContainers(entityCLRType.FullName, exceptionParameterName);
            entitySet = setFromContainer;
          }
        }
      }
      else
        entitySet = this.GetEntitySetFromContainer(defaultContainer, entityCLRType, exceptionParameterName);
      if (entitySet == null)
        throw EntityUtil.NoEntitySetFoundForType(entityCLRType.FullName, exceptionParameterName);
      else
        return entitySet;
    }

    public ObjectSet<TEntity> CreateObjectSet<TEntity>(string entitySetName) where TEntity : class
    {
      return new ObjectSet<TEntity>(this.GetEntitySetForNameAndType(entitySetName, typeof (TEntity), "TEntity"), this);
    }

    private EntitySet GetEntitySetForNameAndType(string entitySetName, Type entityCLRType, string exceptionParameterName)
    {
      EntitySet entitySetFromName = this.GetEntitySetFromName(entitySetName);
      EdmType edmType = this.GetTypeUsage(entityCLRType).EdmType;
      if (entitySetFromName.ElementType != edmType)
        throw EntityUtil.InvalidEntityTypeForObjectSet(entityCLRType.FullName, entitySetFromName.ElementType.FullName, entitySetName, exceptionParameterName);
      else
        return entitySetFromName;
    }

    internal void EnsureConnection()
    {
      if (this._connection == null)
        throw EntityUtil.ObjectContextDisposed();
      if (this.Connection.State == ConnectionState.Closed)
      {
        this.Connection.Open();
        this._openedConnection = true;
      }
      if (this._openedConnection)
        ++this._connectionRequestCount;
      if (this._connection.State != ConnectionState.Closed)
      {
        if (this._connection.State != ConnectionState.Broken)
        {
          try
          {
            this.EnsureMetadata();
            Transaction current = Transaction.Current;
            if ((Transaction) null != current && !current.Equals((object) this._lastTransaction) || (Transaction) null != this._lastTransaction && !this._lastTransaction.Equals((object) current))
            {
              if (!this._openedConnection)
              {
                if (current != (Transaction) null)
                  this._connection.EnlistTransaction(current);
              }
              else if (this._connectionRequestCount > 1)
              {
                if ((Transaction) null == this._lastTransaction)
                {
                  this._connection.EnlistTransaction(current);
                }
                else
                {
                  this._connection.Close();
                  this._connection.Open();
                  this._openedConnection = true;
                  ++this._connectionRequestCount;
                }
              }
            }
            this._lastTransaction = current;
            return;
          }
          catch (Exception ex)
          {
            this.ReleaseConnection();
            throw;
          }
        }
      }
      throw EntityUtil.InvalidOperation(System.Data.Entity.Strings.EntityClient_ExecutingOnClosedConnection(this._connection.State == ConnectionState.Closed ? (object) System.Data.Entity.Strings.EntityClient_ConnectionStateClosed : (object) System.Data.Entity.Strings.EntityClient_ConnectionStateBroken));
    }

    internal void ReleaseConnection()
    {
      if (this._connection == null)
        throw EntityUtil.ObjectContextDisposed();
      if (!this._openedConnection)
        return;
      if (this._connectionRequestCount > 0)
        --this._connectionRequestCount;
      if (this._connectionRequestCount != 0)
        return;
      this.Connection.Close();
      this._openedConnection = false;
    }

    internal void EnsureMetadata()
    {
      if (this.MetadataWorkspace.IsItemCollectionAlreadyRegistered(DataSpace.SSpace))
        return;
      if (this._connection == null)
        throw EntityUtil.ObjectContextDisposed();
      MetadataWorkspace metadataWorkspace = this._connection.GetMetadataWorkspace();
      if (!object.ReferenceEquals((object) metadataWorkspace.GetItemCollection(DataSpace.CSpace), (object) this.MetadataWorkspace.GetItemCollection(DataSpace.CSpace)))
        throw EntityUtil.ContextMetadataHasChanged();
      this.MetadataWorkspace.RegisterItemCollection(metadataWorkspace.GetItemCollection(DataSpace.SSpace));
      this.MetadataWorkspace.RegisterItemCollection(metadataWorkspace.GetItemCollection(DataSpace.CSSpace));
    }

    public ObjectQuery<T> CreateQuery<T>(string queryString, params ObjectParameter[] parameters)
    {
      EntityUtil.CheckArgumentNull<string>(queryString, "queryString");
      EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
      this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof (T), Assembly.GetCallingAssembly());
      ObjectQuery<T> objectQuery = new ObjectQuery<T>(queryString, this, MergeOption.AppendOnly);
      foreach (ObjectParameter parameter in parameters)
        objectQuery.Parameters.Add(parameter);
      return objectQuery;
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public void DeleteObject(object entity)
    {
      this.DeleteObject(entity, (EntitySet) null);
    }

    internal void DeleteObject(object entity, EntitySet expectedEntitySet)
    {
      EntityUtil.CheckArgumentNull<object>(entity, "entity");
      EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
      if (entityEntry == null || !object.ReferenceEquals(entityEntry.Entity, entity))
        throw EntityUtil.CannotDeleteEntityNotInObjectStateManager();
      if (expectedEntitySet != null)
      {
        EntitySetBase entitySet = entityEntry.EntitySet;
        if (entitySet != expectedEntitySet)
          throw EntityUtil.EntityNotInObjectSet_Delete(entitySet.EntityContainer.Name, entitySet.Name, expectedEntitySet.EntityContainer.Name, expectedEntitySet.Name);
      }
      ((ObjectStateEntry) entityEntry).Delete();
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public void Detach(object entity)
    {
      this.Detach(entity, (EntitySet) null);
    }

    internal void Detach(object entity, EntitySet expectedEntitySet)
    {
      EntityUtil.CheckArgumentNull<object>(entity, "entity");
      EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
      if (entityEntry == null || !object.ReferenceEquals(entityEntry.Entity, entity) || entityEntry.Entity == null)
        throw EntityUtil.CannotDetachEntityNotInObjectStateManager();
      if (expectedEntitySet != null)
      {
        EntitySetBase entitySet = entityEntry.EntitySet;
        if (entitySet != expectedEntitySet)
          throw EntityUtil.EntityNotInObjectSet_Detach(entitySet.EntityContainer.Name, entitySet.Name, expectedEntitySet.EntityContainer.Name, expectedEntitySet.Name);
      }
      entityEntry.Detach();
    }

    public void Dispose()
    {
      GC.SuppressFinalize((object) this);
      this.Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this._connection != null)
      {
        this._connection.StateChange -= new StateChangeEventHandler(this.ConnectionStateChange);
        if (this._createdConnection)
          this._connection.Dispose();
      }
      this._connection = (EntityConnection) null;
      this._adapter = (IEntityAdapter) null;
      if (this._cache == null)
        return;
      this._cache.Dispose();
    }

    internal EntitySet GetEntitySet(string entitySetName, string entityContainerName)
    {
      EntityContainer entityContainer = (EntityContainer) null;
      if (string.IsNullOrEmpty(entityContainerName))
        entityContainer = this.Perspective.GetDefaultContainer();
      else if (!this.MetadataWorkspace.TryGetEntityContainer(entityContainerName, DataSpace.CSpace, out entityContainer))
        throw EntityUtil.EntityContainterNotFoundForName(entityContainerName);
      EntitySet entitySet = (EntitySet) null;
      if (!entityContainer.TryGetEntitySetByName(entitySetName, false, out entitySet))
        throw EntityUtil.EntitySetNotFoundForName(TypeHelpers.GetFullName(entityContainer.Name, entitySetName));
      else
        return entitySet;
    }

    internal TypeUsage GetTypeUsage(Type entityCLRType)
    {
      this.MetadataWorkspace.ImplicitLoadAssemblyForType(entityCLRType, Assembly.GetCallingAssembly());
      TypeUsage outTypeUsage = (TypeUsage) null;
      if (!this.Perspective.TryGetType(entityCLRType, out outTypeUsage) || !TypeSemantics.IsEntityType(outTypeUsage))
        throw EntityUtil.InvalidEntityType(entityCLRType);
      else
        return outTypeUsage;
    }

    public object GetObjectByKey(EntityKey key)
    {
      EntityUtil.CheckArgumentNull<EntityKey>(key, "key");
      this.MetadataWorkspace.ImplicitLoadFromEntityType(key.GetEntitySet(this.MetadataWorkspace).ElementType, Assembly.GetCallingAssembly());
      object obj;
      if (!this.TryGetObjectByKey(key, out obj))
        throw EntityUtil.ObjectNotFound();
      else
        return obj;
    }

    public void Refresh(RefreshMode refreshMode, IEnumerable collection)
    {
      EntityUtil.CheckArgumentRefreshMode(refreshMode);
      EntityUtil.CheckArgumentNull<IEnumerable>(collection, "collection");
      this.RefreshEntities(refreshMode, collection);
    }

    public void Refresh(RefreshMode refreshMode, object entity)
    {
      EntityUtil.CheckArgumentRefreshMode(refreshMode);
      EntityUtil.CheckArgumentNull<object>(entity, "entity");
      this.RefreshEntities(refreshMode, (IEnumerable) new object[1]
      {
        entity
      });
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public int SaveChanges()
    {
      return this.SaveChanges(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave);
    }

    [Obsolete("Use SaveChanges(SaveOptions options) instead.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public int SaveChanges(bool acceptChangesDuringSave)
    {
      return this.SaveChanges(acceptChangesDuringSave ? SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave : SaveOptions.DetectChangesBeforeSave);
    }

    public virtual int SaveChanges(SaveOptions options)
    {
      this.OnSavingChanges();
      if ((SaveOptions.DetectChangesBeforeSave & options) != SaveOptions.None)
        this.ObjectStateManager.DetectChanges();
      if (this.ObjectStateManager.SomeEntryWithConceptualNullExists())
        throw new InvalidOperationException(System.Data.Entity.Strings.ObjectContext_CommitWithConceptualNull);
      bool flag1 = false;
      int num = this.ObjectStateManager.GetObjectStateEntriesCount(EntityState.Added | EntityState.Deleted | EntityState.Modified);
      EntityConnection entityConnection = (EntityConnection) this.Connection;
      if (0 < num)
      {
        if (this._adapter == null)
        {
          IServiceProvider serviceProvider = DbProviderFactories.GetFactory((DbConnection) entityConnection) as IServiceProvider;
          if (serviceProvider != null)
            this._adapter = serviceProvider.GetService(typeof (IEntityAdapter)) as IEntityAdapter;
          if (this._adapter == null)
            throw EntityUtil.InvalidDataAdapter();
        }
        this._adapter.AcceptChangesDuringUpdate = false;
        this._adapter.Connection = (DbConnection) entityConnection;
        this._adapter.CommandTimeout = this.CommandTimeout;
        try
        {
          this.EnsureConnection();
          flag1 = true;
          bool flag2 = false;
          if (entityConnection.CurrentTransaction == null && !entityConnection.EnlistedInUserTransaction)
            flag2 = (Transaction) null == this._lastTransaction;
          DbTransaction dbTransaction = (DbTransaction) null;
          try
          {
            if (flag2)
              dbTransaction = (DbTransaction) entityConnection.BeginTransaction();
            num = this._adapter.Update((IEntityStateManager) this.ObjectStateManager);
            if (dbTransaction != null)
              dbTransaction.Commit();
          }
          finally
          {
            if (dbTransaction != null)
              dbTransaction.Dispose();
          }
        }
        finally
        {
          if (flag1)
            this.ReleaseConnection();
        }
        if ((SaveOptions.AcceptAllChangesAfterSave & options) != SaveOptions.None)
        {
          try
          {
            this.AcceptAllChanges();
          }
          catch (Exception ex)
          {
            if (EntityUtil.IsCatchableExceptionType(ex))
              throw EntityUtil.AcceptAllChangesFailure(ex);
            throw;
          }
        }
      }
      return num;
    }

    public void DetectChanges()
    {
      this.ObjectStateManager.DetectChanges();
    }

    public bool TryGetObjectByKey(EntityKey key, out object value)
    {
      EntityEntry entry;
      this.ObjectStateManager.TryGetEntityEntry(key, out entry);
      if (entry != null && !entry.IsKeyEntry)
      {
        value = entry.Entity;
        return value != null;
      }
      else if (key.IsTemporary)
      {
        value = (object) null;
        return false;
      }
      else
      {
        EntitySet entitySet = key.GetEntitySet(this.MetadataWorkspace);
        key.ValidateEntityKey(this._workspace, entitySet, true, "key");
        this.MetadataWorkspace.ImplicitLoadFromEntityType(entitySet.ElementType, Assembly.GetCallingAssembly());
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("SELECT VALUE X FROM {0}.{1} AS X WHERE ", (object) EntityUtil.QuoteIdentifier(entitySet.EntityContainer.Name), (object) EntityUtil.QuoteIdentifier(entitySet.Name));
        EntityKeyMember[] entityKeyValues = key.EntityKeyValues;
        ReadOnlyMetadataCollection<EdmMember> keyMembers = entitySet.ElementType.KeyMembers;
        ObjectParameter[] objectParameterArray = new ObjectParameter[entityKeyValues.Length];
        for (int index = 0; index < entityKeyValues.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(" AND ");
          string name = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "p{0}", new object[1]
          {
            (object) index.ToString((IFormatProvider) CultureInfo.InvariantCulture)
          });
          stringBuilder.AppendFormat("X.{0} = @{1}", (object) EntityUtil.QuoteIdentifier(entityKeyValues[index].Key), (object) name);
          objectParameterArray[index] = new ObjectParameter(name, entityKeyValues[index].Value);
          EdmMember edmMember = (EdmMember) null;
          if (keyMembers.TryGetValue(entityKeyValues[index].Key, true, out edmMember))
            objectParameterArray[index].TypeUsage = edmMember.TypeUsage;
        }
        object obj1 = (object) null;
        foreach (object obj2 in this.CreateQuery<object>(((object) stringBuilder).ToString(), objectParameterArray).Execute(MergeOption.AppendOnly))
          obj1 = obj2;
        value = obj1;
        return value != null;
      }
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters)
    {
      return this.ExecuteFunction<TElement>(functionName, MergeOption.AppendOnly, parameters);
    }

    public ObjectResult<TElement> ExecuteFunction<TElement>(string functionName, MergeOption mergeOption, params ObjectParameter[] parameters)
    {
      EntityUtil.CheckStringArgument(functionName, "function");
      EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
      EdmFunction functionImport;
      EntityCommand forFunctionImport = this.CreateEntityCommandForFunctionImport(functionName, out functionImport, parameters);
      int length = Math.Max(1, functionImport.ReturnParameters.Count);
      EdmType[] edmTypes = new EdmType[length];
      edmTypes[0] = MetadataHelper.GetAndCheckFunctionImportReturnType<TElement>(functionImport, 0, this.MetadataWorkspace);
      for (int resultSetIndex = 1; resultSetIndex < length; ++resultSetIndex)
      {
        if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(functionImport, resultSetIndex, out edmTypes[resultSetIndex]))
          throw EntityUtil.ExecuteFunctionCalledWithNonReaderFunction(functionImport);
      }
      return this.CreateFunctionObjectResult<TElement>(forFunctionImport, functionImport.EntitySets, edmTypes, mergeOption);
    }

    public int ExecuteFunction(string functionName, params ObjectParameter[] parameters)
    {
      EntityUtil.CheckStringArgument(functionName, "function");
      EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
      EdmFunction functionImport;
      EntityCommand forFunctionImport = this.CreateEntityCommandForFunctionImport(functionName, out functionImport, parameters);
      this.EnsureConnection();
      forFunctionImport.Prepare();
      try
      {
        return forFunctionImport.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        if (EntityUtil.IsCatchableEntityExceptionType(ex))
          throw EntityUtil.CommandExecution(System.Data.Entity.Strings.EntityClient_CommandExecutionFailed, ex);
        throw;
      }
      finally
      {
        this.ReleaseConnection();
      }
    }

    private EntityCommand CreateEntityCommandForFunctionImport(string functionName, out EdmFunction functionImport, params ObjectParameter[] parameters)
    {
      for (int index = 0; index < parameters.Length; ++index)
      {
        if (parameters[index] == null)
          throw EntityUtil.InvalidOperation(System.Data.Entity.Strings.ObjectContext_ExecuteFunctionCalledWithNullParameter((object) index));
      }
      string containerName;
      string functionImportName;
      functionImport = MetadataHelper.GetFunctionImport(functionName, this.DefaultContainerName, this.MetadataWorkspace, out containerName, out functionImportName);
      EntityConnection entityConnection = (EntityConnection) this.Connection;
      EntityCommand command = new EntityCommand();
      command.CommandType = CommandType.StoredProcedure;
      command.CommandText = containerName + "." + functionImportName;
      command.Connection = entityConnection;
      if (this.CommandTimeout.HasValue)
        command.CommandTimeout = this.CommandTimeout.Value;
      this.PopulateFunctionImportEntityCommandParameters(parameters, functionImport, command);
      return command;
    }

    private ObjectResult<TElement> CreateFunctionObjectResult<TElement>(EntityCommand entityCommand, ReadOnlyMetadataCollection<EntitySet> entitySets, EdmType[] edmTypes, MergeOption mergeOption)
    {
      this.EnsureConnection();
      EntityCommandDefinition commandDefinition = entityCommand.GetCommandDefinition();
      DbDataReader storeReader;
      try
      {
        storeReader = commandDefinition.ExecuteStoreCommands(entityCommand, CommandBehavior.Default);
      }
      catch (Exception ex)
      {
        this.ReleaseConnection();
        if (EntityUtil.IsCatchableEntityExceptionType(ex))
          throw EntityUtil.CommandExecution(System.Data.Entity.Strings.EntityClient_CommandExecutionFailed, ex);
        throw;
      }
      return this.MaterializedDataRecord<TElement>(entityCommand, storeReader, 0, entitySets, edmTypes, mergeOption);
    }

    internal ObjectResult<TElement> MaterializedDataRecord<TElement>(EntityCommand entityCommand, DbDataReader storeReader, int resultSetIndex, ReadOnlyMetadataCollection<EntitySet> entitySets, EdmType[] edmTypes, MergeOption mergeOption)
    {
      EntityCommandDefinition commandDefinition = entityCommand.GetCommandDefinition();
      try
      {
        bool readerOwned = edmTypes.Length <= resultSetIndex + 1;
        EdmType edmType = edmTypes[resultSetIndex];
        EntitySet singleEntitySet = entitySets.Count > resultSetIndex ? ((ReadOnlyCollection<EntitySet>) entitySets)[resultSetIndex] : (EntitySet) null;
        Shaper<TElement> shaper = System.Data.Common.Internal.Materialization.Translator.TranslateColumnMap<TElement>(this.Perspective.MetadataWorkspace.GetQueryCacheManager(), commandDefinition.CreateColumnMap(storeReader, resultSetIndex), this.MetadataWorkspace, (SpanIndex) null, mergeOption, false).Create(storeReader, this, this.MetadataWorkspace, mergeOption, readerOwned);
        bool onReaderDisposeHasRun = false;
        Action<object, EventArgs> onReaderDispose = (Action<object, EventArgs>) ((sender, e) =>
        {
          if (onReaderDisposeHasRun)
            return;
          onReaderDisposeHasRun = true;
          CommandHelper.ConsumeReader(storeReader);
          entityCommand.NotifyDataReaderClosing();
        });
        NextResultGenerator nextResultGenerator;
        if (readerOwned)
        {
          shaper.OnDone += new EventHandler(onReaderDispose.Invoke);
          nextResultGenerator = (NextResultGenerator) null;
        }
        else
          nextResultGenerator = new NextResultGenerator(this, entityCommand, edmTypes, entitySets, mergeOption, resultSetIndex + 1);
        return new ObjectResult<TElement>(shaper, singleEntitySet, TypeUsage.Create(edmTypes[resultSetIndex]), true, nextResultGenerator, onReaderDispose);
      }
      catch
      {
        this.ReleaseConnection();
        storeReader.Dispose();
        throw;
      }
    }

    public void CreateProxyTypes(IEnumerable<Type> types)
    {
      ObjectItemCollection ospaceItems = (ObjectItemCollection) this.MetadataWorkspace.GetItemCollection(DataSpace.OSpace);
      EntityProxyFactory.TryCreateProxyTypes(Enumerable.Where<EntityType>(Enumerable.Select<Type, EntityType>(types, (Func<Type, EntityType>) (type =>
      {
        this.MetadataWorkspace.ImplicitLoadAssemblyForType(type, (Assembly) null);
        EntityType local_0;
        ospaceItems.TryGetItem<EntityType>(type.FullName, out local_0);
        return local_0;
      })), (Func<EntityType, bool>) (entityType => entityType != null)));
    }

    public static IEnumerable<Type> GetKnownProxyTypes()
    {
      return EntityProxyFactory.GetKnownProxyTypes();
    }

    public static Type GetObjectType(Type type)
    {
      EntityUtil.CheckArgumentNull<Type>(type, "type");
      if (!EntityProxyFactory.IsProxyType(type))
        return type;
      else
        return type.BaseType;
    }

    public T CreateObject<T>() where T : class
    {
      T obj1 = default (T);
      Type type = typeof (T);
      this.MetadataWorkspace.ImplicitLoadAssemblyForType(type, (Assembly) null);
      ClrEntityType clrEntityType = this.MetadataWorkspace.GetItem<ClrEntityType>(type.FullName, DataSpace.OSpace);
      EntityProxyTypeInfo proxyType;
      T obj2;
      if (this.ContextOptions.ProxyCreationEnabled && (proxyType = EntityProxyFactory.GetProxyType(clrEntityType)) != null)
      {
        obj2 = (T) proxyType.CreateProxyObject();
        IEntityWrapper newWrapper = EntityWrapperFactory.CreateNewWrapper((object) obj2, (EntityKey) null);
        newWrapper.InitializingProxyRelatedEnds = true;
        try
        {
          newWrapper.AttachContext(this, (EntitySet) null, MergeOption.NoTracking);
          proxyType.SetEntityWrapper(newWrapper);
          if ((MethodInfo) proxyType.InitializeEntityCollections != (MethodInfo) null)
            proxyType.InitializeEntityCollections.Invoke((object) null, new object[1]
            {
              (object) newWrapper
            });
        }
        finally
        {
          newWrapper.InitializingProxyRelatedEnds = false;
          newWrapper.DetachContext();
        }
      }
      else
        obj2 = (LightweightCodeGenerator.GetConstructorDelegateForType(clrEntityType) as Func<object>)() as T;
      return obj2;
    }

    public int ExecuteStoreCommand(string commandText, params object[] parameters)
    {
      this.EnsureConnection();
      try
      {
        return this.CreateStoreCommand(commandText, parameters).ExecuteNonQuery();
      }
      finally
      {
        this.ReleaseConnection();
      }
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters)
    {
      return this.ExecuteStoreQueryInternal<TElement>(commandText, (string) null, MergeOption.AppendOnly, parameters);
    }

    public ObjectResult<TEntity> ExecuteStoreQuery<TEntity>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters)
    {
      EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
      return this.ExecuteStoreQueryInternal<TEntity>(commandText, entitySetName, mergeOption, parameters);
    }

    private ObjectResult<TElement> ExecuteStoreQueryInternal<TElement>(string commandText, string entitySetName, MergeOption mergeOption, params object[] parameters)
    {
      this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof (TElement), Assembly.GetCallingAssembly());
      this.EnsureConnection();
      DbDataReader reader;
      try
      {
        reader = this.CreateStoreCommand(commandText, parameters).ExecuteReader();
      }
      catch
      {
        this.ReleaseConnection();
        throw;
      }
      try
      {
        return this.InternalTranslate<TElement>(reader, entitySetName, mergeOption, true);
      }
      catch
      {
        reader.Dispose();
        this.ReleaseConnection();
        throw;
      }
    }

    public ObjectResult<TElement> Translate<TElement>(DbDataReader reader)
    {
      this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof (TElement), Assembly.GetCallingAssembly());
      return this.InternalTranslate<TElement>(reader, (string) null, MergeOption.AppendOnly, false);
    }

    public ObjectResult<TEntity> Translate<TEntity>(DbDataReader reader, string entitySetName, MergeOption mergeOption)
    {
      EntityUtil.CheckStringArgument(entitySetName, "entitySetName");
      this.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof (TEntity), Assembly.GetCallingAssembly());
      return this.InternalTranslate<TEntity>(reader, entitySetName, mergeOption, false);
    }

    private ObjectResult<TElement> InternalTranslate<TElement>(DbDataReader reader, string entitySetName, MergeOption mergeOption, bool readerOwned)
    {
      EntityUtil.CheckArgumentNull<DbDataReader>(reader, "reader");
      EntityUtil.CheckArgumentMergeOption(mergeOption);
      EntitySet entitySet = (EntitySet) null;
      if (!string.IsNullOrEmpty(entitySetName))
        entitySet = this.GetEntitySetFromName(entitySetName);
      this.EnsureMetadata();
      Type type = Nullable.GetUnderlyingType(typeof (TElement)) ?? typeof (TElement);
      EdmType modelEdmType;
      CollectionColumnMap collectionColumnMap;
      if (MetadataHelper.TryDetermineCSpaceModelType<TElement>(this.MetadataWorkspace, out modelEdmType) || type.IsEnum && MetadataHelper.TryDetermineCSpaceModelType(type.GetEnumUnderlyingType(), this.MetadataWorkspace, out modelEdmType))
      {
        if (entitySet != null && !entitySet.ElementType.IsAssignableFrom(modelEdmType))
          throw EntityUtil.InvalidOperation(System.Data.Entity.Strings.ObjectContext_InvalidEntitySetForStoreQuery((object) entitySet.EntityContainer.Name, (object) entitySet.Name, (object) typeof (TElement)));
        collectionColumnMap = ColumnMapFactory.CreateColumnMapFromReaderAndType(reader, modelEdmType, entitySet, (Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping>) null);
      }
      else
        collectionColumnMap = ColumnMapFactory.CreateColumnMapFromReaderAndClrType(reader, typeof (TElement), this.MetadataWorkspace);
      return new ObjectResult<TElement>(System.Data.Common.Internal.Materialization.Translator.TranslateColumnMap<TElement>(this.MetadataWorkspace.GetQueryCacheManager(), (ColumnMap) collectionColumnMap, this.MetadataWorkspace, (SpanIndex) null, mergeOption, false).Create(reader, this, this.MetadataWorkspace, mergeOption, readerOwned), entitySet, MetadataHelper.GetElementType(collectionColumnMap.Type), readerOwned);
    }

    private DbCommand CreateStoreCommand(string commandText, params object[] parameters)
    {
      DbCommand command = this._connection.StoreConnection.CreateCommand();
      command.CommandText = commandText;
      if (this.CommandTimeout.HasValue)
        command.CommandTimeout = this.CommandTimeout.Value;
      EntityTransaction currentTransaction = this._connection.CurrentTransaction;
      if (currentTransaction != null)
        command.Transaction = currentTransaction.StoreTransaction;
      if (parameters != null && parameters.Length > 0)
      {
        DbParameter[] dbParameterArray = new DbParameter[parameters.Length];
        if (Enumerable.All<object>((IEnumerable<object>) parameters, (Func<object, bool>) (p => p is DbParameter)))
        {
          for (int index = 0; index < parameters.Length; ++index)
            dbParameterArray[index] = (DbParameter) parameters[index];
        }
        else
        {
          if (Enumerable.Any<object>((IEnumerable<object>) parameters, (Func<object, bool>) (p => p is DbParameter)))
            throw EntityUtil.InvalidOperation(System.Data.Entity.Strings.ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues);
          string[] strArray1 = new string[parameters.Length];
          string[] strArray2 = new string[parameters.Length];
          for (int index = 0; index < parameters.Length; ++index)
          {
            strArray1[index] = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "p{0}", new object[1]
            {
              (object) index
            });
            dbParameterArray[index] = command.CreateParameter();
            dbParameterArray[index].ParameterName = strArray1[index];
            dbParameterArray[index].Value = parameters[index] ?? (object) DBNull.Value;
            strArray2[index] = "@" + strArray1[index];
          }
          command.CommandText = string.Format((IFormatProvider) CultureInfo.InvariantCulture, command.CommandText, (object[]) strArray2);
        }
        command.Parameters.AddRange((Array) dbParameterArray);
      }
      return command;
    }

    public void CreateDatabase()
    {
      DbProviderServices.GetProviderServices(this.GetStoreItemCollection().StoreProviderFactory).CreateDatabase(this._connection.StoreConnection, this.CommandTimeout, this.GetStoreItemCollection());
    }

    public void DeleteDatabase()
    {
      DbProviderServices.GetProviderServices(this.GetStoreItemCollection().StoreProviderFactory).DeleteDatabase(this._connection.StoreConnection, this.CommandTimeout, this.GetStoreItemCollection());
    }

    public bool DatabaseExists()
    {
      return DbProviderServices.GetProviderServices(this.GetStoreItemCollection().StoreProviderFactory).DatabaseExists(this._connection.StoreConnection, this.CommandTimeout, this.GetStoreItemCollection());
    }

    public string CreateDatabaseScript()
    {
      return DbProviderServices.GetProviderServices(this.GetStoreItemCollection().StoreProviderFactory).CreateDatabaseScript(this.GetStoreItemCollection().StoreProviderManifestToken, this.GetStoreItemCollection());
    }

    private void OnSavingChanges()
    {
      if (this._onSavingChanges == null)
        return;
      this._onSavingChanges((object) this, new EventArgs());
    }

    private void VerifyRootForAdd(bool doAttach, string entitySetName, IEntityWrapper wrappedEntity, EntityEntry existingEntry, out EntitySet entitySet, out bool isNoOperation)
    {
      isNoOperation = false;
      EntitySet entitySet1 = (EntitySet) null;
      if (doAttach)
      {
        if (!string.IsNullOrEmpty(entitySetName))
          entitySet1 = this.GetEntitySetFromName(entitySetName);
      }
      else
        entitySet1 = this.GetEntitySetFromName(entitySetName);
      EntitySet entitySet2 = (EntitySet) null;
      EntityKey key = existingEntry != null ? existingEntry.EntityKey : wrappedEntity.GetEntityKeyFromEntity();
      if (key != null)
      {
        entitySet2 = key.GetEntitySet(this.MetadataWorkspace);
        if (entitySet1 != null)
          EntityUtil.ValidateEntitySetInKey(key, entitySet1, "entitySetName");
        key.ValidateEntityKey(this._workspace, entitySet2);
      }
      entitySet = entitySet2 ?? entitySet1;
      if (entitySet == null)
        throw EntityUtil.EntitySetNameOrEntityKeyRequired();
      this.ValidateEntitySet(entitySet, wrappedEntity.IdentityType);
      if (doAttach && existingEntry == null)
      {
        if (key == null)
          key = this.ObjectStateManager.CreateEntityKey(entitySet, wrappedEntity.Entity);
        existingEntry = this.ObjectStateManager.FindEntityEntry(key);
      }
      if (existingEntry == null || doAttach && existingEntry.IsKeyEntry)
        return;
      if (!object.ReferenceEquals(existingEntry.Entity, wrappedEntity.Entity))
        throw EntityUtil.ObjectStateManagerContainsThisEntityKey();
      EntityState entityState = doAttach ? EntityState.Unchanged : EntityState.Added;
      if (existingEntry.State != entityState)
        throw doAttach ? EntityUtil.EntityAlreadyExistsInObjectStateManager() : EntityUtil.ObjectStateManagerDoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(existingEntry.State);
      isNoOperation = true;
    }

    private void VerifyContextForAddOrAttach(IEntityWrapper wrappedEntity)
    {
      if (wrappedEntity.Context != null && wrappedEntity.Context != this && (!wrappedEntity.Context.ObjectStateManager.IsDisposed && wrappedEntity.MergeOption != MergeOption.NoTracking))
        throw EntityUtil.EntityCantHaveMultipleChangeTrackers();
    }

    private void AddRefreshKey(object entityLike, Dictionary<EntityKey, EntityEntry> entities, Dictionary<EntitySet, List<EntityKey>> currentKeys)
    {
      if (entityLike == null)
        throw EntityUtil.NthElementIsNull(entities.Count);
      EntityKey entityKey = EntityWrapperFactory.WrapEntityUsingContext(entityLike, this).EntityKey;
      this.RefreshCheck(entities, entityLike, entityKey);
      EntitySet entitySet = entityKey.GetEntitySet(this.MetadataWorkspace);
      List<EntityKey> list = (List<EntityKey>) null;
      if (!currentKeys.TryGetValue(entitySet, out list))
      {
        list = new List<EntityKey>();
        currentKeys.Add(entitySet, list);
      }
      list.Add(entityKey);
    }

    private EntitySet GetEntitySetFromContainer(EntityContainer container, Type entityCLRType, string exceptionParameterName)
    {
      EdmType edmType = this.GetTypeUsage(entityCLRType).EdmType;
      EntitySet entitySet = (EntitySet) null;
      foreach (EntitySetBase entitySetBase in container.BaseEntitySets)
      {
        if (entitySetBase.BuiltInTypeKind == BuiltInTypeKind.EntitySet && entitySetBase.ElementType == edmType)
        {
          if (entitySet != null)
            throw EntityUtil.MultipleEntitySetsFoundInSingleContainer(entityCLRType.FullName, container.Name, exceptionParameterName);
          entitySet = (EntitySet) entitySetBase;
        }
      }
      return entitySet;
    }

    private void ConnectionStateChange(object sender, StateChangeEventArgs e)
    {
      if (e.CurrentState != ConnectionState.Closed)
        return;
      this._connectionRequestCount = 0;
      this._openedConnection = false;
    }

    private static EntityConnection CreateEntityConnection(string connectionString)
    {
      EntityUtil.CheckStringArgument(connectionString, "connectionString");
      return new EntityConnection(connectionString);
    }

    private MetadataWorkspace RetrieveMetadataWorkspaceFromConnection()
    {
      if (this._connection == null)
        throw EntityUtil.ObjectContextDisposed();
      else
        return this._connection.GetMetadataWorkspace(false).ShallowCopy();
    }

    private static void GetEntitySetName(string qualifiedName, string parameterName, ObjectContext context, out string entityset, out string container)
    {
      entityset = (string) null;
      container = (string) null;
      EntityUtil.CheckStringArgument(qualifiedName, parameterName);
      string[] strArray = qualifiedName.Split(new char[1]
      {
        '.'
      });
      if (strArray.Length > 2)
        throw EntityUtil.QualfiedEntitySetName(parameterName);
      if (strArray.Length == 1)
      {
        entityset = strArray[0];
      }
      else
      {
        container = strArray[0];
        entityset = strArray[1];
        if (container == null || container.Length == 0)
          throw EntityUtil.QualfiedEntitySetName(parameterName);
      }
      if (entityset == null || entityset.Length == 0)
        throw EntityUtil.QualfiedEntitySetName(parameterName);
      if (context != null && string.IsNullOrEmpty(container) && context.Perspective.GetDefaultContainer() == null)
        throw EntityUtil.ContainerQualifiedEntitySetNameRequired(parameterName);
    }

    private void ValidateEntitySet(EntitySet entitySet, Type entityType)
    {
      TypeUsage typeUsage = this.GetTypeUsage(entityType);
      if (!entitySet.ElementType.IsAssignableFrom(typeUsage.EdmType))
        throw EntityUtil.InvalidEntitySetOnEntity(entitySet.Name, entityType, "entity");
    }

    private void RefreshCheck(Dictionary<EntityKey, EntityEntry> entities, object entity, EntityKey key)
    {
      EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(key);
      if (entityEntry == null)
        throw EntityUtil.NthElementNotInObjectStateManager(entities.Count);
      if (EntityState.Added == entityEntry.State)
        throw EntityUtil.NthElementInAddedState(entities.Count);
      try
      {
        entities.Add(key, entityEntry);
      }
      catch (ArgumentException ex)
      {
        throw EntityUtil.NthElementIsDuplicate(entities.Count);
      }
    }

    private void RefreshEntities(RefreshMode refreshMode, IEnumerable collection)
    {
      bool flag = false;
      try
      {
        Dictionary<EntityKey, EntityEntry> dictionary = new Dictionary<EntityKey, EntityEntry>(ObjectContext.RefreshEntitiesSize(collection));
        Dictionary<EntitySet, List<EntityKey>> currentKeys = new Dictionary<EntitySet, List<EntityKey>>();
        foreach (object entityLike in collection)
          this.AddRefreshKey(entityLike, dictionary, currentKeys);
        collection = (IEnumerable) null;
        if (currentKeys.Count > 0)
        {
          this.EnsureConnection();
          flag = true;
          foreach (EntitySet targetSet in currentKeys.Keys)
          {
            List<EntityKey> targetKeys = currentKeys[targetSet];
            int startFrom = 0;
            while (startFrom < targetKeys.Count)
              startFrom = this.BatchRefreshEntitiesByKey(refreshMode, dictionary, targetSet, targetKeys, startFrom);
          }
        }
        if (RefreshMode.StoreWins == refreshMode)
        {
          foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair in dictionary)
          {
            if (EntityState.Detached != keyValuePair.Value.State)
            {
              this.ObjectStateManager.TransactionManager.BeginDetaching();
              try
              {
                ((ObjectStateEntry) keyValuePair.Value).Delete();
              }
              finally
              {
                this.ObjectStateManager.TransactionManager.EndDetaching();
              }
              keyValuePair.Value.AcceptChanges();
            }
          }
        }
        else
        {
          if (RefreshMode.ClientWins != refreshMode || 0 >= dictionary.Count)
            return;
          string str = string.Empty;
          StringBuilder stringBuilder = new StringBuilder();
          foreach (KeyValuePair<EntityKey, EntityEntry> keyValuePair in dictionary)
          {
            if (keyValuePair.Value.State == EntityState.Deleted)
            {
              keyValuePair.Value.AcceptChanges();
            }
            else
            {
              stringBuilder.Append(str).Append(Environment.NewLine);
              stringBuilder.Append('\'').Append(keyValuePair.Key.ConcatKeyValue()).Append('\'');
              str = ",";
            }
          }
          if (stringBuilder.Length > 0)
            throw EntityUtil.ClientEntityRemovedFromStore(((object) stringBuilder).ToString());
        }
      }
      finally
      {
        if (flag)
          this.ReleaseConnection();
      }
    }

    private int BatchRefreshEntitiesByKey(RefreshMode refreshMode, Dictionary<EntityKey, EntityEntry> trackedEntities, EntitySet targetSet, List<EntityKey> targetKeys, int startFrom)
    {
      DbExpressionBinding input = DbExpressionBuilder.BindAs((DbExpression) DbExpressionBuilder.Scan((EntitySetBase) targetSet), "EntitySet");
      DbExpression left = (DbExpression) DbExpressionBuilder.GetRefKey((DbExpression) DbExpressionBuilder.GetEntityRef((DbExpression) input.Variable));
      int length = Math.Min(250, targetKeys.Count - startFrom);
      DbExpression[] dbExpressionArray = new DbExpression[length];
      for (int index = 0; index < length; ++index)
      {
        DbExpression right = (DbExpression) DbExpressionBuilder.NewRow((IEnumerable<KeyValuePair<string, DbExpression>>) targetKeys[startFrom++].GetKeyValueExpressions(targetSet));
        dbExpressionArray[index] = (DbExpression) DbExpressionBuilder.Equal(left, right);
      }
      DbExpression predicate = Helpers.BuildBalancedTreeInPlace<DbExpression>((IList<DbExpression>) dbExpressionArray, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Or));
      DbQueryCommandTree query = DbQueryCommandTree.FromValidExpression(this.MetadataWorkspace, DataSpace.CSpace, (DbExpression) DbExpressionBuilder.Filter(input, predicate));
      MergeOption mergeOption = RefreshMode.StoreWins == refreshMode ? MergeOption.OverwriteChanges : MergeOption.PreserveChanges;
      this.EnsureConnection();
      try
      {
        foreach (object entity in ObjectQueryExecutionPlan.ExecuteCommandTree<object>(this, query, mergeOption))
        {
          EntityEntry entityEntry = this.ObjectStateManager.FindEntityEntry(entity);
          if (entityEntry != null && EntityState.Modified == entityEntry.State)
            entityEntry.SetModifiedAll();
          EntityKey entityKey = EntityWrapperFactory.WrapEntityUsingContext(entity, this).EntityKey;
          EntityUtil.CheckEntityKeyNull(entityKey);
          if (!trackedEntities.Remove(entityKey))
            throw EntityUtil.StoreEntityNotPresentInClient();
        }
      }
      catch
      {
        this.ReleaseConnection();
        throw;
      }
      return startFrom;
    }

    private static int RefreshEntitiesSize(IEnumerable collection)
    {
      ICollection collection1 = collection as ICollection;
      if (collection1 == null)
        return 0;
      else
        return collection1.Count;
    }

    private void PopulateFunctionImportEntityCommandParameters(ObjectParameter[] parameters, EdmFunction functionImport, EntityCommand command)
    {
      for (int ordinal = 0; ordinal < parameters.Length; ++ordinal)
      {
        ObjectParameter objectParameter = parameters[ordinal];
        EntityParameter entityParameter = new EntityParameter();
        FunctionParameter parameterMetadata = ObjectContext.FindParameterMetadata(functionImport, parameters, ordinal);
        if (parameterMetadata != null)
        {
          entityParameter.Direction = MetadataHelper.ParameterModeToParameterDirection(parameterMetadata.Mode);
          entityParameter.ParameterName = parameterMetadata.Name;
        }
        else
          entityParameter.ParameterName = objectParameter.Name;
        entityParameter.Value = objectParameter.Value ?? (object) DBNull.Value;
        if (DBNull.Value == entityParameter.Value || entityParameter.Direction != ParameterDirection.Input)
        {
          TypeUsage typeUsage;
          if (parameterMetadata != null)
            typeUsage = parameterMetadata.TypeUsage;
          else if (objectParameter.TypeUsage == null)
          {
            if (!this.Perspective.TryGetTypeByName(objectParameter.MappableType.FullName, false, out typeUsage))
            {
              this.MetadataWorkspace.ImplicitLoadAssemblyForType(objectParameter.MappableType, (Assembly) null);
              this.Perspective.TryGetTypeByName(objectParameter.MappableType.FullName, false, out typeUsage);
            }
          }
          else
            typeUsage = objectParameter.TypeUsage;
          EntityCommandDefinition.PopulateParameterFromTypeUsage(entityParameter, typeUsage, entityParameter.Direction != ParameterDirection.Input);
        }
        if (entityParameter.Direction != ParameterDirection.Input)
        {
          ObjectContext.ParameterBinder parameterBinder = new ObjectContext.ParameterBinder(entityParameter, objectParameter);
          command.OnDataReaderClosing += new EventHandler(parameterBinder.OnDataReaderClosingHandler);
        }
        command.Parameters.Add(entityParameter);
      }
    }

    private static FunctionParameter FindParameterMetadata(EdmFunction functionImport, ObjectParameter[] parameters, int ordinal)
    {
      string name = parameters[ordinal].Name;
      FunctionParameter functionParameter;
      if (!functionImport.Parameters.TryGetValue(name, false, out functionParameter))
      {
        int num = 0;
        for (int index = 0; index < parameters.Length && num < 2; ++index)
        {
          if (StringComparer.OrdinalIgnoreCase.Equals(parameters[index].Name, name))
            ++num;
        }
        if (num == 1)
          functionImport.Parameters.TryGetValue(name, true, out functionParameter);
      }
      return functionParameter;
    }

    private StoreItemCollection GetStoreItemCollection()
    {
      return (StoreItemCollection) ((EntityConnection) this.Connection).GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
    }

    private class ParameterBinder
    {
      private readonly EntityParameter _entityParameter;
      private readonly ObjectParameter _objectParameter;

      internal ParameterBinder(EntityParameter entityParameter, ObjectParameter objectParameter)
      {
        this._entityParameter = entityParameter;
        this._objectParameter = objectParameter;
      }

      internal void OnDataReaderClosingHandler(object sender, EventArgs args)
      {
        if (this._entityParameter.Value != DBNull.Value && this._objectParameter.MappableType.IsEnum)
          this._objectParameter.Value = Enum.ToObject(this._objectParameter.MappableType, this._entityParameter.Value);
        else
          this._objectParameter.Value = this._entityParameter.Value;
      }
    }
  }
}
