// Type: AuthorizeNet.Gateway
// Assembly: AuthorizeNet, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: D:\work\work maged\Lemo\Lemo\Lemo\bin\AuthorizeNet.dll

using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace AuthorizeNet
{
  public class Gateway : IGateway
  {
    public const string TEST_URL = "https://test.authorize.net/gateway/transact.dll";
    public const string LIVE_URL = "https://secure.authorize.net/gateway/transact.dll";

    public string ApiLogin { get; set; }

    public string TransactionKey { get; set; }

    public bool TestMode { get; set; }

    public Gateway(string apiLogin, string transactionKey, bool testMode)
    {
      this.ApiLogin = apiLogin;
      this.TransactionKey = transactionKey;
      this.TestMode = testMode;
    }

    public Gateway(string apiLogin, string transactionKey)
      : this(apiLogin, transactionKey, true)
    {
    }

    public IGatewayResponse Send(IGatewayRequest request)
    {
      return this.Send(request, (string) null);
    }

    protected void LoadAuthorization(IGatewayRequest request)
    {
      request.Queue("x_login", this.ApiLogin);
      request.Queue("x_tran_key", this.TransactionKey);
    }

    protected string SendRequest(string serviceUrl, IGatewayRequest request)
    {
      string str1 = request.ToPostString();
      string str2 = "";
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(serviceUrl);
      httpWebRequest.Method = "POST";
      httpWebRequest.ContentLength = (long) str1.Length;
      httpWebRequest.ContentType = "application/x-www-form-urlencoded";
      StreamWriter streamWriter = new StreamWriter(((WebRequest) httpWebRequest).GetRequestStream());
      streamWriter.Write(str1);
      streamWriter.Close();
      using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
      {
        str2 = streamReader.ReadToEnd();
        streamReader.Close();
      }
      return str2;
    }

    public virtual IGatewayResponse Send(IGatewayRequest request, string description)
    {
      string serviceUrl = "https://test.authorize.net/gateway/transact.dll";
      if (!this.TestMode)
        serviceUrl = "https://secure.authorize.net/gateway/transact.dll";
      this.LoadAuthorization(request);
      if (string.IsNullOrEmpty(request.Description))
        request.Queue("x_description", description);
      return this.DecideResponse(this.SendRequest(serviceUrl, request).Split(new char[1]
      {
        '|'
      }));
    }

    public IGatewayResponse DecideResponse(string[] rawResponse)
    {
      if (rawResponse.Length == 1)
        throw new InvalidDataException("There was an error returned from AuthorizeNet: " + rawResponse[0] + "; this usually means your data sent along was incorrect. Please recheck that all dates and amounts are formatted correctly");
      else
        return (IGatewayResponse) new GatewayResponse(rawResponse);
    }

    private class PolicyOverride : ICertificatePolicy
    {
      bool ICertificatePolicy.CheckValidationResult(ServicePoint srvPoint, X509Certificate cert, WebRequest request, int certificateProblem)
      {
        return true;
      }
    }
  }
}
