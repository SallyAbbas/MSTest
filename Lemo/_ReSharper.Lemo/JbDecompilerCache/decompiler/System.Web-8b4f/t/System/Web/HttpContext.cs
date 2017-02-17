// Type: System.Web.HttpContext
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Web.dll

using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
  public sealed class HttpContext : IServiceProvider
  {
    internal const int FLAG_ETW_PROVIDER_ENABLED = 64;
    internal static readonly Assembly SystemWebAssembly;
    internal ProfileBase _Profile;
    internal bool _skipAuthorization;
    internal bool HideRequestResponse;
    internal volatile bool InIndicateCompletion;
    internal volatile HttpApplication.ThreadContext IndicateCompletionContext;
    internal bool InAspCompatMode;
    internal bool _ProfileDelayLoad;

    internal bool RequiresSessionState { get; }

    internal bool ReadOnlySessionState { get; }

    public static HttpContext Current { get; set; }

    internal IntPtr ContextPtr { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; }

    internal IHttpAsyncHandler AsyncAppHandler { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    public HttpApplication ApplicationInstance { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; set; }

    public HttpApplicationState Application { get; }

    public IHttpHandler Handler { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; set; }

    public IHttpHandler PreviousHandler { get; }

    public IHttpHandler CurrentHandler { get; }

    internal IHttpHandler RemapHandlerInstance { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; }

    public HttpRequest Request { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; }

    public HttpResponse Response { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; }

    internal IHttpHandler TopHandler { get; }

    public TraceContext Trace { get; }

    internal bool TraceIsEnabled { get; set; }

    public IDictionary Items { get; }

    public HttpSessionState Session { get; }

    public HttpServerUtility Server { get; }

    public Exception Error { get; }

    internal Exception TempError { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    public Exception[] AllErrors { get; }

    public IPrincipal User { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries"), SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)] set; }

    public ProfileBase Profile { get; }

    internal SessionStateBehavior SessionStateBehavior { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    public bool SkipAuthorization { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries"), SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)] set; }

    public bool IsDebuggingEnabled { get; }

    public bool IsCustomErrorEnabled { get; }

    internal TemplateControl TemplateControl { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    public DateTime Timestamp { get; }

    internal DateTime UtcTimestamp { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; }

    internal HttpWorkerRequest WorkerRequest { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; }

    public Cache Cache { get; }

    internal VirtualPath ConfigurationPath { get; set; }

    internal CultureInfo DynamicCulture { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    internal CultureInfo DynamicUICulture { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    internal int ServerExecuteDepth { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    internal bool PreventPostback { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    internal Thread CurrentThread { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    internal TimeSpan Timeout { get; set; }

    internal DoubleLink TimeoutLink { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    internal bool IsInCancellablePeriod { get; }

    internal CookielessHelperClass CookielessHelper { get; }

    internal string SqlDependencyCookie { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; set; }

    internal NotificationContext NotificationContext { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get; [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set; }

    public RequestNotification CurrentNotification { get; internal set; }

    internal bool IsChangeInServerVars { get; }

    internal bool IsChangeInRequestHeaders { get; }

    internal bool IsChangeInResponseHeaders { get; }

    internal bool IsChangeInResponseStatus { get; }

    internal bool IsChangeInUserPrincipal { get; }

    internal bool IsSendResponseHeaders { get; }

    internal bool UsesImpersonation { get; }

    internal bool AreResponseHeadersSent { get; }

    internal int CurrentNotificationFlags { get; set; }

    internal int CurrentModuleIndex { get; set; }

    internal int CurrentModuleEventIndex { get; set; }

    public bool IsPostNotification { get; internal set; }

    internal IntPtr ClientIdentityToken { get; }

    internal bool IsClientImpersonationConfigured { get; }

    internal IntPtr ImpersonationToken { get; }

    internal AspNetSynchronizationContext SyncContext { [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")] get; }

    static HttpContext();

    public HttpContext(HttpRequest request, HttpResponse response);

    public HttpContext(HttpWorkerRequest wr);

    internal HttpContext(HttpWorkerRequest wr, bool initResponseWriter);

    internal void Root();

    internal void Unroot();

    internal void FinishPipelineRequest();

    internal void ValidatePath();

    object IServiceProvider.GetService(Type service);

    internal void RestoreCurrentHandler();

    internal void SetCurrentHandler(IHttpHandler newtHandler);

    public void RemapHandler(IHttpHandler handler);

    internal void AddDelayedHttpSessionState(SessionStateModule module);

    internal void RemoveDelayedHttpSessionState();

    internal void ReportRuntimeErrorIfExists(ref RequestNotificationStatus status);

    public void AddError(Exception errorInfo);

    public void ClearError();

    internal void SetPrincipalNoDemand(IPrincipal principal, bool needToSetNativePrincipal);

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    internal void SetPrincipalNoDemand(IPrincipal principal);

    public void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior);

    internal void SetSkipAuthorizationNoDemand(bool value, bool managedOnly);

    internal CachedPathData GetFilePathData();

    internal CachedPathData GetConfigurationPathData();

    internal CachedPathData GetPathData(VirtualPath path);

    internal void FinishRequestForCachedPathData(int statusCode);

    [Obsolete("The recommended alternative is System.Web.Configuration.WebConfigurationManager.GetWebApplicationSection in System.Web.dll. http://go.microsoft.com/fwlink/?linkid=14202")]
    public static object GetAppConfig(string name);

    [Obsolete("The recommended alternative is System.Web.HttpContext.GetSection in System.Web.dll. http://go.microsoft.com/fwlink/?linkid=14202")]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public object GetConfig(string name);

    public object GetSection(string sectionName);

    internal RuntimeConfig GetRuntimeConfig();

    internal RuntimeConfig GetRuntimeConfig(VirtualPath path);

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public void RewritePath(string path);

    public void RewritePath(string path, bool rebaseClientPath);

    public void RewritePath(string filePath, string pathInfo, string queryString);

    public void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath);

    internal void RewritePath(VirtualPath filePath, VirtualPath pathInfo, string queryString, bool setClientFilePath);

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static object GetGlobalResourceObject(string classKey, string resourceKey);

    public static object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture);

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static object GetLocalResourceObject(string virtualPath, string resourceKey);

    public static object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture);

    internal void EnsureTimeout();

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    internal void BeginCancellablePeriod();

    internal void SetStartTime();

    internal void EndCancellablePeriod();

    [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    internal void WaitForExceptionIfCancelled();

    internal Thread MustTimeout(DateTime utcNow);

    internal void InvokeCancellableCallback(WaitCallback callback, object state);

    internal void PushTraceContext();

    internal void PopTraceContext();

    internal bool RequestRequiresAuthorization();

    internal int CallISAPI(UnsafeNativeMethods.CallISAPIFunc iFunction, byte[] bufIn, byte[] bufOut);

    internal void SendEmptyResponse();

    internal void ResetSqlDependencyCookie();

    internal void RemoveSqlDependencyCookie();

    internal void SetImpersonationEnabled();

    internal bool NeedToInitializeApp();

    internal void DisableNotifications(RequestNotification notifications, RequestNotification postNotifications);

    internal void DisposePrincipal();

    internal AspNetSynchronizationContext InstallNewAspNetSynchronizationContext();

    internal void RestoreSavedAspNetSynchronizationContext(AspNetSynchronizationContext syncContext);

    internal string UserLanguageFromContext();

    internal void ClearReferences();

    internal CultureInfo CultureFromConfig(string configString, bool requireSpecific);
  }
}
