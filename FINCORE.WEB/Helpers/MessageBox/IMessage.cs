using Microsoft.AspNetCore.Mvc;

namespace FINCORE.WEB.Helpers.MessageBox
{
    public interface IMessage
    {
        /// <summary>
        /// Show Alert Message Error or Failed
        /// </summary>
        /// <param name="message">Alert Message</param>
        /// <param name="viewName">View name with specific location path. Exp: ~/Views/Acquisition/CAS/Index.cshtml</param>
        /// <returns></returns>
        public IActionResult AlertError(string message, string viewName);

        /// <summary>
        /// Show Alert Message Success
        /// </summary>
        /// <param name="message">Alert Message</param>
        /// <param name="viewName">View name with specific location path. Exp: ~/Views/Acquisition/CAS/Index.cshtml</param>
        /// <returns></returns>
        public IActionResult AlertSuccess(string message, string viewName);

        /// <summary>
        /// Show Alert Message Information
        /// </summary>
        /// <param name="message">Alert Message</param>
        /// <param name="viewName">View name with specific location path. Exp: ~/Views/Acquisition/CAS/Index.cshtml</param>
        /// <returns></returns>
        public IActionResult AlertInfo(string message, string viewName);

        /// <summary>
        /// Show Alert Message Warning
        /// </summary>
        /// <param name="message">Alert Message</param>
        /// <param name="viewName">View name with specific location path. Exp: ~/Views/Acquisition/CAS/Index.cshtml</param>
        /// <returns></returns>
        public IActionResult AlertWarning(string message, string viewName);

        /// <summary>
        /// Alert Success and direct goto other page, Refresh, Back with IActionResult function
        /// </summary>
        /// <param name="message"></param>
        /// <param name="directTo"></param>
        /// <returns></returns>
        public IActionResult AlertSuccessThenDirect(string message, IActionResult directTo);

        /// <summary>
        /// Alert Info and direct goto other page, Refresh, Back with IActionResult function
        /// </summary>
        /// <param name="message"></param>
        /// <param name="directTo"></param>
        /// <returns></returns>
        public IActionResult AlertInfoThenDirect(string message, IActionResult directTo);

        /// <summary>
        /// Alert Warning and direct goto other page, Refresh, Back with IActionResult function
        /// </summary>
        /// <param name="message"></param>
        /// <param name="directTo"></param>
        /// <returns></returns>
        public IActionResult AlertWarningThenDirect(string message, IActionResult directTo);
    }
}