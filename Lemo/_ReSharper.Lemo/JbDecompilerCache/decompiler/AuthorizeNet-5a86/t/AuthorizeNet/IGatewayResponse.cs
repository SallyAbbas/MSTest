// Type: AuthorizeNet.IGatewayResponse
// Assembly: AuthorizeNet, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: D:\work\work maged\Lemo\Lemo\Lemo\bin\AuthorizeNet.dll

using System;

namespace AuthorizeNet
{
  public interface IGatewayResponse
  {
    Decimal Amount { get; }

    bool Approved { get; }

    string AuthorizationCode { get; }

    string InvoiceNumber { get; }

    string CardNumber { get; }

    string ResponseCode { get; }

    string Message { get; }

    string TransactionID { get; }
  }
}
