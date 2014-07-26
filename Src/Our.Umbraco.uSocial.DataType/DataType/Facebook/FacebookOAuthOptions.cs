namespace Our.Umbraco.uSocial.DataType.Facebook
{
    /// <summary>
    /// Options for the Facebook OAuth authentication data type
    /// </summary>
    public class FacebookOAuthOptions
    {
        /// <summary>
        /// Gets or sets the app id.
        /// </summary>
        /// <value>
        /// The app id.
        /// </value>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the app secret.
        /// </summary>
        /// <value>
        /// The app secret.
        /// </value>
		public string AppSecret { get; set; }

		/// <summary>
		/// Gets or sets the client token.
		/// </summary>
		/// <value>
		/// The client token.
		/// </value>
		public string ClientToken { get; set; }

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>
        /// The data format.
        /// </value>
        public DataFormat DataFormat { get; set; }
    }
}
