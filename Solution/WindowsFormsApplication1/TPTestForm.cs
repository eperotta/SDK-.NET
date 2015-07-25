using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodoPagoConnector;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.IO;



namespace WindowsFormsApplication1
{
    public partial class TPTestForm : Form
    {

        private const string SECURITY = "Security";
        private const string SESSION = "Session";
        private const string MERCHANT = "Merchant";
        private const string REQUESTKEY = "RequestKey";
        private const string ANSWERKEY = "AnswerKey";
        private const string URL_OK = "URL OK";
        private const string URL_ERROR = "URL Error";
        private const string ENCODING_METHOD = "Encoding Method";



        public TPTestForm()
        {
            InitializeComponent();
        }

        private TPConnector initConnector()
        {
            var headers = new Dictionary<String, String>();

            String auth = tbGCAuth.Text;
            headers.Add("Authorization", auth);

            headers.Add("USERAGENT", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");
            headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");
            headers.Add("UserAgent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");

            String endpoint = tbGCEp.Text;

            return new TPConnector(endpoint, headers);
        }


        /// <summary>
        /// Permite emular la validación del Certificado SSL devolviendo true siempre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns>bool true</returns>
        private bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }



        private void btnGetAuth_Click(object sender, EventArgs e)
        {
            try
            {


                lResult.Text = String.Empty;
                lDetail.Text = String.Empty;

                var connector = initConnector();
                var request = new Dictionary<String, String>();

                request.Add(SECURITY, tbGAASec.Text);
                request.Add(SESSION, null);
                request.Add(MERCHANT, tbGAAMerc.Text);
                request.Add(REQUESTKEY, tbGAAReq.Text);
                request.Add(ANSWERKEY, tbGAAAnsw.Text);

                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificate);

                var res = connector.GetAuthorizeAnswer(request);

                lResult.Text = res["StatusCode"].ToString() + "-" + res["StatusMessage"].ToString();
                lDetail.Text = "AuthorizationKey = " + res["AuthorizationKey"] + "\r\nEncodingMethod = " + res["EncodingMethod"] + "\r\nPayload = " + res["Payload"].ToString() + "\r\n";

                System.Xml.XmlNode[] aux = (System.Xml.XmlNode[])res["Payload"];
                //lDetail.Text += aux.Count();
                for (int i = 0; i < aux.Count(); i++)
                {
                    lDetail.Text += aux[i].InnerXml.ToString();
                }


            }
            catch (Exception ex)
            {
                lDetail.Text = ex.Message;
            }
        }

        private void btnSetAuth_Click(object sender, EventArgs e)
        {
            try{
                lResult.Text = string.Empty;
                lDetail.Text = string.Empty;

                var connector = initConnector();

                var request = new Dictionary<string, string>();
                request.Add(SECURITY, tbSARSec.Text);
                request.Add(SESSION, tbSARSes.Text);
                request.Add(MERCHANT, tbSARMerc.Text);
                request.Add(URL_OK, tbSAROk.Text);
                request.Add(URL_ERROR, tbSARErr.Text);
                request.Add(ENCODING_METHOD, tbSAREnc.Text);

                var payload = new Dictionary<string, string>();
                payload.Add("MERCHANT", tbSARMerc.Text);
                payload.Add("OPERATIONID", tbSAROp.Text);
                payload.Add("CURRENCYCODE", tbSARCurr.Text);
                payload.Add("AMOUNT", tbSARAm.Text);
                payload.Add("EMAILCLIENTE", tbSARMail.Text);

                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificate);

                var res = connector.SendAuthorizeRequest(request, payload);

                lResult.Text = res["StatusCode"].ToString() + "-" + res["StatusMessage"].ToString();
                lDetail.Text = "URL_Request = " + res["URL_Request"] + "\r\nRequestKey = " + res["RequestKey"] + "\r\nPublicRequestKey = " + res["PublicRequestKey"];

            }
            catch (Exception ex)
            {
                lDetail.Text = ex.Message;
            }
        }

        private void btnGetStat_Click(object sender, EventArgs e)
        {
            try
            {
                lResult.Text = string.Empty;
                lDetail.Text = string.Empty;

                var connector = initConnector();

                System.Net.ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificate);

                String merchant = tbGSMerc.Text;
                String operationID = tbGSOp.Text;
                var res = connector.GetStatus(merchant, operationID);

                lResult.Text = res.Count().ToString();
                foreach (var operation in res)
                {
                    foreach (var propName in operation.Keys)
                    {
                        lDetail.Text += propName + ": " + operation[propName] + "\r\n";
                    }

                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    WebResponse resp = ex.Response;
                    using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                    {

                        lDetail.Text = sr.ReadToEnd() + " \n  " + ex.Message;
                        //Response.Write(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                lDetail.Text += ex.Message + " \n " + ex.InnerException.Message + " \n " + ex.HelpLink;
            }
        }
    }
}
