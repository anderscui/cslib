#region Comments

// Function£ºProvide some common functions in web develop.
// Authur£ºAnders Cui
// Date£º2007-03-23

// Modify by£ºAnders Cui
// Modify Date£º2007-04-01
// Modify content£ºadd get QueryString method

#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Andersc.CodeLib.Common.WebForm;

namespace Andersc.CodeLib.Common
{
    /// <summary>
    /// Provides some common functions in web develop
    /// </summary>
    /// <remarks>Include status management,cache,page information,etc.</remarks>
    public class WebHelper
    {
        public static readonly string DDL_DEFAUT_ITEM = "Please Select";

        public const string CONTENT_TYPE_JPEG = "image/jpeg";
        public const string CONTENT_TYPE_PJPEG = "image/pjpeg";
        public const string CONTENT_TYPE_GIF = "image/gif";
        public const string CONTENT_TYPE_BMP = "image/bmp";

        public const string CONTENT_TYPE_WORD = "application/msword";
        public const string CONTENT_TYPE_PDF = "application/pdf";
        public const string CONTENT_TYPE_TEXT = "text/plain";

        // ms excel 8
        public const string CONTENT_TYPE_EXCEL = "application/vnd.ms-excel";

        // office 2007
        public const string CONTENT_TYPE_WORD2007 = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        private WebHelper()
        {
        }

        #region QueryString Management

        /// <summary>
        /// Gets the value of QueryString.
        /// </summary>
        /// <param name="name">QueryString name</param>
        /// <returns></returns>
        public static string GetQueryString(string name)
        {
            NameValueCollection queries = HttpContext.Current.Request.QueryString;
            return queries[name];
        }

        /// <summary>
        /// Gets the full query string of current page.
        /// </summary>
        /// <returns></returns>
        public static string GetFullQueryString()
        {
            return CurrentServerVariables()["query_string"];
        }

        #endregion

        #region Cookie Management

        /// <summary>
        /// Creates a Cookie entry
        /// </summary>
        /// <param name="cookieName">Cookie entry name</param>
        /// <param name="value">The value of Cookie</param>
        public static void SetCookie(string cookieName, string value)
        {
            SetCookie(cookieName, value, DateTime.Now.AddHours(4));
        }

        /// <summary>
        /// Creates a Cookie entry and set the deadline.
        /// </summary>
        /// <param name="cookieName">Cookie entry name</param>
        /// <param name="value">value of Cookie</param>
        /// <param name="expires">Cookie deadline</param>
        public static void SetCookie(string cookieName, string value, DateTime expires)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "The value of Cookie can not be null¡£");
            }

            HttpCookie cookie = new HttpCookie(cookieName, value);
            cookie.Expires = expires;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Gets value of the specific Cookie
        /// </summary>
        /// <param name="cookieName">value of the Cookie</param>
        /// <returns>If the value exist return TRUE otherwise tetrun null¡£</returns>
        public static string GetCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookieName);
            if (cookie == null)
            {
                return null;
            }
            else
            {
                return cookie.Value;
            }
        }

        /// <summary>
        /// Gets value of the specific Cookie
        /// </summary>
        /// <param name="cookieName">value of the Cookie</param>
        /// <returns></returns>
        public static HttpCookie GetCookieItem(string cookieName)
        {
            return HttpContext.Current.Request.Cookies.Get(cookieName);
        }

        /// <summary>
        /// deletes the value of the specific Cookie
        /// </summary>
        /// <param name="cookieName">Cookie item's name</param>
        public static void RemoveCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookieName);
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddHours(-1);
            }
        }

        #endregion

        #region Session Management

        /// <summary>
        /// Writes Session's value
        /// </summary>
        /// <param name="key">Session's key</param>
        /// <param name="value">Session's value</param>
        /// <remarks>recommend use strongly type of session </remarks>
        public static void SetSession(string key, object value)
        {
            HttpContext.Current.Session.Add(key, value);
        }

        /// <summary>
        /// Gets a Session's value
        /// </summary>
        /// <param name="key">Session's key to be get</param>
        /// <returns></returns>
        public static object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }

        /// <summary>
        /// removes a session item 
        /// </summary>
        /// <param name="key">session key to be removed</param>
        public static void RemoveSession(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        #endregion

        #region Cache Management

        /// <summary>
        /// Adds a cache item. 
        /// </summary>
        /// <param name="key">cache key</param>
        /// <param name="value">cache value</param>
        public static void SetCache(string key, object value)
        {
            HttpContext.Current.Cache.Insert(key, value);
        }

        /// <summary>
        /// Adds a cache item. 
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <param name="value">Cache value</param>
        /// <param name="fileName">The file name of cache item</param>
        /// <remarks>Use to dependence between buicache item and file , for instance an XML file.</remarks>
        public static void SetCache(string key, object value, string fileName)
        {
            HttpContext.Current.Cache.Insert(key, value, new CacheDependency(fileName));
        }

        /// <summary>
        /// Adds a cache item.
        /// </summary>
        /// <param name="key">Cache's key</param>
        /// <param name="value">cache value</param>
        /// <param name="dependency">cache dependence</param>
        public static void SetCache(string key, object value, CacheDependency dependency)
        {
            HttpContext.Current.Cache.Insert(key, value, dependency);
        }

        /// <summary>
        /// Adds a cache item.
        /// </summary>
        /// <param name="key">Key of cache item</param>
        /// <param name="value">value of cache item</param>
        /// <param name="expiration">expiration time</param>
        public static void SetCache(string key, object value, DateTime expiration)
        {
            HttpContext.Current.Cache.Insert(key, value, null, expiration, Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Adds a cache item.
        /// </summary>
        /// <param name="key">Key of cache item</param>
        /// <param name="value">value of cache item</param>
        /// <param name="slidingExpiration">sliding expiration of cache</param>
        public static void SetCache(string key, object value, TimeSpan slidingExpiration)
        {
            HttpContext.Current.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// Adds a cache item.
        /// </summary>
        /// <param name="key">Key of cache item</param>
        /// <param name="value">value of cache item</param>
        /// <param name="dependency">cache dependency</param>
        /// <param name="expiration">cache expiration</param>
        /// <param name="slidingExpiration">sliding expiration of cache</param>
        /// <param name="priority">priority of cache</param>
        /// <param name="onRemove">call back of cache</param>
        public static void SetCache(string key, object value, CacheDependency dependency, DateTime expiration, TimeSpan slidingExpiration,
            CacheItemPriority priority, CacheItemRemovedCallback onRemove)
        {
            HttpContext.Current.Cache.Insert(key, value, null, expiration, slidingExpiration, priority, onRemove);
        }

        /// <summary>
        /// Gets key's cache item.
        /// </summary>
        /// <param name="key">Key value</param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return HttpContext.Current.Cache.Get(key);
        }

        // TODO: Add Scavenging overloads.

        // TODO: Add CallbackSupport overloads.

        /// <summary>
        /// Removes specific key's cache item.
        /// </summary>
        public static void RemoveCache(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>
        /// Gets available cache space.
        /// </summary>
        /// <returns></returns>
        public static long GetAvailableCacheSpace()
        {
            return HttpContext.Current.Cache.EffectivePrivateBytesLimit;
        }

        #endregion

        #region Application Management


        #endregion

        #region Page Info

        public static NameValueCollection CurrentServerVariables()
        {
            return HttpContext.Current.Request.ServerVariables;
        }

        /// <summary>
        /// Get requested page's full path.
        /// </summary>
        /// <returns>Url of current page.</returns>
        public static string GetCurrentPageFullPath()
        {
            string url = CurrentServerVariables()["url"];
            string queryString = CurrentServerVariables()["query_string"];

            if (string.IsNullOrEmpty(queryString))
            {
                return url;
            }
            else
            {
                return url + "?" + queryString;
            }
        }

        public static string GetCurrentPagePath()
        {
            return CurrentServerVariables()["url"];
        }

        /// <summary>
        /// Get file name of requested page.
        /// </summary>
        /// <returns>current page's file name.</returns>
        public static string GetCurrentPageName()
        {
            string requestdPath = HttpContext.Current.Request.Path;

            return Path.GetFileName(requestdPath);
        }

        /// <summary>
        /// Gets the current page's physical path.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentPagePhysicalPath()
        {
            return HttpContext.Current.Server.MapPath("");
        }

        /// <summary>
        /// Get current client's IP address.
        /// </summary>
        /// <returns>Return client's IP address</returns>
        public static string GetClientIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }


        /// <summary>
        /// Validates all validators on a page.
        /// </summary>
        /// <param name="page">The page.</param>
        public static void ValidateAll(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page", "Page can not be empty.");
            }

            //enable all verifications
            foreach (BaseValidator va in page.Validators)
            {
                va.Enabled = true;
            }

            //Verify controls in pages
            page.Validate();
        }


        /// <summary>
        /// Disables the validators on a page.
        /// </summary>
        /// <param name="page">The page.</param>
        public static void DisableValidators(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page", "Page can not be empty.");
            }

            foreach (BaseValidator va in page.Validators)
            {
                va.Enabled = false;
            }
        }

        /// <summary>
        /// Shows the validating error with the default error message header 'Validating Error'.
        /// </summary>
        /// <param name="page">The page.</param>
        public static void ShowValidateError(Page page)
        {
            ShowValidateError(page, "Validating Error");
        }

        /// <summary>
        /// Shows the validating error with specified error message header.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="errorHeader">The error message header.</param>
        public static void ShowValidateError(Page page, string errorHeader)
        {
            string errorMessage = errorHeader + @":\n\n";

            foreach (BaseValidator va in page.Validators)
            {
                if (!va.IsValid)
                {
                    errorMessage += va.ErrorMessage + @"\n";
                }
            }
            errorMessage += @"\n";

            ScriptHelper.Alert(page, "ValidatorError", errorMessage);

            DisableValidators(page);
        }

        #endregion

        #region Application Info

        /// <summary>
        /// Get current ASP.NET application's absolute path
        /// </summary>
        /// <returns>absolute path of application</returns>
        public static string AppPath()
        {
            return HttpRuntime.AppDomainAppPath;
        }

        /// <summary>
        /// Get current ASP.NET application's absolute path of bin folder.
        /// </summary>
        /// <returns>absolute path of bin folder</returns>
        public static string AppBinDirectory()
        {
            return HttpRuntime.BinDirectory;
        }

        /// <summary>
        /// Gets current web app's base path.
        /// </summary>
        /// <returns></returns>
        public static string GetAppBasePath()
        {
            HttpRequest request = HttpContext.Current.Request;

            string port = request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
            {
                port = "";
            }
            else
            {
                port = ":" + port;
            }

            string Protocol = request.ServerVariables["SERVER_PORT_SECURE"];
            if (Protocol == null || Protocol == "0")
            {
                Protocol = "http://";
            }
            else
            {
                Protocol = "https://";
            }

            string path = Protocol + request.ServerVariables["SERVER_NAME"] + port + request.ApplicationPath;
            if (!path.EndsWith("/"))
            {
                path += "/";
            }

            return path;
        }

        #endregion

        #region Encode

        public static string HtmlEncode(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        /// <summary>
        /// </summary>
        /// <param name="input">encoded string</param>
        /// <returns>decoded string</returns>
        public static string HtmlDecode(string input)
        {
            return HttpUtility.HtmlDecode(input);
        }

        /// <summary>
        /// Coding URL string and transfer between Web server and client without error.
        /// </summary>
        /// <param name="url">string waitting to be code</param>
        /// <returns>edcoded string</returns>
        public static string UrlEncode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        /// <summary>
        /// Convert edcoded string to decoded string which defined in URI
        /// </summary>
        /// <param name="url">pending string</param>
        /// <returns>decoded string.</returns>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        /// <summary>
        /// Method to make sure that user's inputs are not malicious
        /// </summary>
        /// <param name="text">User's Input</param>
        /// <param name="maxLength">Maximum length of input</param>
        /// <returns>The cleaned up version of the input</returns>
        public static string InputText(string text, int maxLength)
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''");
            return text;
        }

        #endregion

        #region Control Manipulation

        private static void BindListControl(ListControl listCtrl, object dataSource, string dataTextField, string dataValueField, string prompt)
        {
            listCtrl.DataTextField = dataTextField;
            listCtrl.DataValueField = dataValueField;
            listCtrl.DataSource = dataSource;
            listCtrl.DataBind();

            // adds a prompt item which value is empty string, so RequiredFieldValidator is available.
            if (prompt != null)
            {
                listCtrl.Items.Insert(0, new ListItem(prompt, ""));
            }
        }

        public static void BindListBox(ListBox listBox, object dataSource, string dataTextField, string dataValueField, string prompt)
        {
            BindListControl(listBox, dataSource, dataTextField, dataValueField, prompt);
        }

        public static void BindListBox(ListBox listBox, object dataSource, string dataTextField, string dataValueField)
        {
            //Modified by Spencer Sun at 17-07-2007
            //BindListBox(listBox, dataSource, dataTextField, dataValueField, DDL_DEFAUT_ITEM);
            BindListBox(listBox, dataSource, dataTextField, dataValueField, null);
        }
        /// <summary>
        /// @author:Sean Wang
        /// @create:2007-7-27
        /// </summary>
        /// <param name="cbl"></param>
        /// <param name="ds"></param>
        /// <param name="txtField"></param>
        /// <param name="valueField"></param>
        public static void BindCheckBoxList(CheckBoxList cbl, object ds, String txtField, String valueField)
        {
            if (cbl == null)
            {
                throw new Exception("The input checkBoxlist is null.");
            }
            cbl.DataSource = ds;
            cbl.DataTextField = txtField;
            cbl.DataValueField = valueField;
            cbl.DataBind();
        }

        /// <summary>
        /// Binds the DropDownList control, and adds a prompt item.
        /// </summary>
        /// <param name="ddl">The DropDownList.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="dataTextField">The dataTextField.</param>
        /// <param name="dataValueField">The dataValueField.</param>
        /// <param name="prompt">The prompt text.</param>
        public static void BindDropDownList(DropDownList ddl, object dataSource, string dataTextField, string dataValueField, string prompt)
        {
            BindListControl(ddl, dataSource, dataTextField, dataValueField, prompt);
        }

        public static void BindDropDownList(DropDownList ddl, object dataSource, string dataTextField, string dataValueField)
        {
            BindDropDownList(ddl, dataSource, dataTextField, dataValueField, DDL_DEFAUT_ITEM);
        }

        /// <summary>
        /// Binds the DropDownList control, and adds a prompt item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ddl">The DropDownList.</param>
        /// <param name="dataSource">The data source.</param>
        /// <param name="dataTextField">The data text field.</param>
        /// <param name="dataValueField">The data value field.</param>
        /// <param name="prompt">The prompt.</param>
        public static void BindDropDownList<T>(DropDownList ddl, IList<T> dataSource, string dataTextField, string dataValueField, string prompt)
        {
            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataValueField;
            ddl.DataSource = dataSource;
            ddl.DataBind();

            // adds a prompt item which value is empty string, so RequiredFieldValidator is available.
            ddl.Items.Insert(0, new ListItem(prompt, ""));
        }

        public static void BindDropDownList<T>(DropDownList ddl, IList<T> dataSource, string dataTextField, string dataValueField)
        {
            BindDropDownList<T>(ddl, dataSource, dataTextField, dataValueField, DDL_DEFAUT_ITEM);
        }

        /// <summary>
        /// Selects the DropDownList item by value.
        /// </summary>
        /// <param name="ddl">The DropDownList.</param>
        /// <param name="value">The value.</param>
        public static void SelectDropDownListItemByValue(DropDownList ddl, string value)
        {
            if (value == null)
            {
                return;
            }

            ListItem item = ddl.Items.FindByValue(value);
            if (item != null)
            {
                item.Selected = true;
            }
        }

        public static void SelectListBoxByValues(ListBox lbx, string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return;
            }

            foreach (string value in values)
            {
                ListItem item = lbx.Items.FindByValue(value);
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }

        public static void SelectCheckBoxListByValues(CheckBoxList cbl, string[] values)
        {
            if (values == null || values.Length == 0)
            {
                return;
            }

            foreach (string value in values)
            {
                ListItem item = cbl.Items.FindByValue(value);
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }

        /// <summary>
        /// Reads binary data from input file component.
        /// </summary>
        /// <returns>binary data (byte[] type) of input file component.</returns>
        public static byte[] ReadInputFile(HtmlInputFile inputFile)
        {
            HttpPostedFile postedFile = inputFile.PostedFile;
            int fileLength = postedFile.ContentLength;

            byte[] data = null;
            if (fileLength > 0)
            {
                // Allocate a buffer for reading of the file.
                data = new byte[fileLength];

                // Read uploaded file from the Stream.
                postedFile.InputStream.Read(data, 0, fileLength);
            }
            return data;
        }

        public static string ReadInputFileContentType(HtmlInputFile inputFile)
        {
            HttpPostedFile postedFile = inputFile.PostedFile;
            int fileLength = postedFile.ContentLength;

            string contentType = null;
            if (fileLength > 0)
            {
                contentType = postedFile.ContentType;
            }
            return contentType;
        }

        public static bool IsJpegImage(string contentType)
        {
            if (string.IsNullOrEmpty(contentType)) { return false; }

            return ((string.Compare(contentType.ToLower(), CONTENT_TYPE_JPEG) == 0) || (string.Compare(contentType.ToLower(), CONTENT_TYPE_PJPEG) == 0));
        }

        public static bool IsGifImage(string contentType)
        {
            if (string.IsNullOrEmpty(contentType)) { return false; }

            return (string.Compare(contentType.ToLower(), CONTENT_TYPE_GIF) == 0);
        }

        public static bool IsWordDoc(string contentType)
        {
            if (string.IsNullOrEmpty(contentType)) { return false; }

            return (contentType.ToLower() == CONTENT_TYPE_WORD);
        }

        public static bool IsWord2007Doc(string contentType)
        {
            if (string.IsNullOrEmpty(contentType)) { return false; }

            return (contentType.ToLower() == CONTENT_TYPE_WORD2007);
        }

        public static bool IsPdfDoc(string contentType)
        {
            if (string.IsNullOrEmpty(contentType)) { return false; }

            return (contentType.ToLower() == CONTENT_TYPE_PDF);
        }

        public static bool IsTextDoc(string contentType)
        {
            if (string.IsNullOrEmpty(contentType)) { return false; }

            return (contentType.ToLower() == CONTENT_TYPE_TEXT);
        }

        #endregion

        /// <summary>
        /// Get selected value in the ListBox
        /// </summary>
        /// <remarks>brucef@everse.com</remarks>
        /// <param name="listbox"></param>
        /// <returns></returns>
        public static IList<string> SelectedListBoxItemValue(ListBox listbox)
        {
            IList<string> values = new List<string>();
            ListItemCollection listItems = listbox.Items;
            foreach (ListItem listItem in listItems)
            {
                if (listItem.Selected)
                    values.Add(listItem.Value);
            }
            return values;
        }

        public static IList<int> SelectedListBoxItemValueInt(ListBox listbox)
        {
            try
            {
                IList<int> values = new List<int>();
                ListItemCollection listItems = listbox.Items;
                foreach (ListItem listItem in listItems)
                {
                    if (listItem.Selected)
                        values.Add(int.Parse(listItem.Value));
                }
                return values;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
