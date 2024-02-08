using System.IO;
using System.Threading.Tasks;
using static indy_vdr_dotnet.models.Structures;

namespace indy_vdr_dotnet.libindy_vdr
{
    public static class ModApi
    {
        /// <summary>
        /// Sets a new configuration to the pool.
        /// </summary>
        /// <param name="config">Pool configuration as JSON string:
        /// <code>
        ///     {
        ///         "protocol_version": [String],
	    ///         "freshness_threshold": [Integer],
	    ///         "ack_timeout": [Integer],
	    ///         "reply_timeout": [Integer],
	    ///         "conn_request_limit":[Integer],
	    ///         "conn_active_timeout": [Integer],
	    ///         "request_read_nodes": [Integer],
	    ///         "socks_proxy": [String - proxy Url]
        ///     }
        /// </code>
        /// </param>
        /// <exception cref="IndyVdrException">Throws if <paramref name="config"/> is invalid.</exception>
        /// <returns><see cref="ErrorCode"/> value of method (<c>0</c> if success).</returns>
        public static async Task<int> SetConfigAsync(
            string config)
        {
            int errorCode = NativeMethods.indy_vdr_set_config(
                FfiStr.Create(config));

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }
            return errorCode;
        }

        /// <summary>
        /// Sets the default logger for pool methods.
        /// </summary>
        /// <exception cref="IndyVdrException">Throws if setting the default logger was not possible.</exception>
        /// <returns><see cref="ErrorCode"/> value of method (<c>0</c> if success).</returns>
        public static async Task<int> SetDefaultLoggerAsync()
        {
            int errorCode = NativeMethods.indy_vdr_set_default_logger();

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }
            return errorCode;
        }

        /// <summary>
        /// Sets the protocol version to be used by pool.
        /// </summary>
        /// <param name="version">Version of protocol (Currently supported: 1 and 2)</param>
        /// <exception cref="IndyVdrException">Throws if <paramref name="version"/> is invalid.</exception>
        /// <returns><see cref="ErrorCode"/> value of method (<c>0</c> if success).</returns>
        public static async Task<int> SetProtocolVersionAsync(long version)
        {
            int errorCode = NativeMethods.indy_vdr_set_protocol_version(version);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }
            return errorCode;
        }

        public static async Task<int> SetCacheDirectoryAsync(string path)
        {
            int errorCode = NativeMethods.indy_vdr_set_cache_directory(FfiStr.Create(path));
            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }
            return errorCode;
        }

        public static async Task<int> SetLedgerTxnCacheAsync(int capacity, long expire_offset, string path_opt)
        {
            int errorCode = NativeMethods.indy_vdr_set_ledger_txn_cache(capacity, expire_offset, FfiStr.Create(path_opt));
            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }
            return errorCode;
        }
        /// <summary>
        /// Sets the socks proxy address used by pool.
        /// </summary>
        /// <param name="socks_proxy">Address of socks proxy (Format: <c>URL:Port</c>)</param>
        /// <exception cref="IndyVdrException">Throws if <paramref name="socks_proxy"/> is invalid.</exception>
        /// <returns><see cref="ErrorCode"/> value of method (<c>0</c> if success).</returns>
        public static async Task<int> SetSocksProxyAsync(string socks_proxy)
        {
            int errorCode = NativeMethods.indy_vdr_set_socks_proxy(FfiStr.Create(socks_proxy));
            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }
            return errorCode;
        }

        /// <summary>
        /// Gets current vdr version.
        /// </summary>
        /// <returns>Currently used version of vdr (Format: <c>x.x.x</c>).</returns>
        public static async Task<string> GetVersionAsync()
        {
            return await Task.FromResult<string>(NativeMethods.indy_vdr_version());
        }
    }
}