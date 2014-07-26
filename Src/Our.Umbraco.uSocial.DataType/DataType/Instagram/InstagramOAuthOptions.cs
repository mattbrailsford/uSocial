namespace Our.Umbraco.uSocial.DataType.Instagram
{
    /// <summary>
    /// Options for the Instagram OAuth authentication data type
    /// </summary>
    public class InstagramOAuthOptions
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>
        /// The client id.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>
        /// The data format.
        /// </value>
        public DataFormat DataFormat { get; set; }
    }
}
