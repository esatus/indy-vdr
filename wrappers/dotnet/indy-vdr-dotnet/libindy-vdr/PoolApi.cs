﻿using indy_vdr_dotnet.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static indy_vdr_dotnet.libindy_vdr.NativeMethods;
using static indy_vdr_dotnet.models.Structures;

namespace indy_vdr_dotnet.libindy_vdr
{
    public class PoolApi
    {
        /// <summary>
        /// Creates and initializes a new pool.
        /// </summary>
        /// <param name="transactions"></param>
        /// <param name="transactionsPath">Path of genesis file.</param>
        /// <param name="nodeWeights">Initial node weights.</param>
        /// <exception cref="IndyVdrException">Throws if any parameter is invalid.</exception>
        /// <returns>Handle of a new pool object.</returns>
        public static async Task<IntPtr> CreatePoolAsync(
            string transactions = null,
            string transactionsPath = null,
            Dictionary<string, float> nodeWeights = null)
        {
            IntPtr poolHandle = new();
            string paramsJson = JsonConvert.SerializeObject(new
            {
                transactions,
                transactions_path = transactionsPath,
                node_weights = nodeWeights
            });

            int errorCode = NativeMethods.indy_vdr_pool_create(
                FfiStr.Create(paramsJson),
                ref poolHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            return poolHandle;
        }

        #region RefreshPool
        /// <summary>
        /// Resfreshes pool transactions.
        /// </summary>
        /// <param name="poolHandle">Handle of pool object.</param>
        /// <exception cref="IndyVdrException">Throws if provided <paramref name="poolHandle"/> is invalid.</exception>
        /// <returns><cTrue</c> if pool could be refreshed, <c>False</c> if not.</returns>
        public static async Task<bool> RefreshPoolAsync(
            IntPtr poolHandle)
        {
            TaskCompletionSource<bool> taskCompletionSource = new();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.indy_vdr_pool_refresh(
                poolHandle,
                PoolRefreshCallback,
                callbackId
                );

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }
        private static void PoolRefreshCallbackMethod(long callbackId, int errorCode)
        {
            TaskCompletionSource<bool> taskCompletionSource = PendingCallbacks.Remove<bool>(callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(IndyVdrException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(true);
        }
        private static readonly PoolRefreshCompletedDelegate PoolRefreshCallback = PoolRefreshCallbackMethod;
        #endregion

        #region GetPoolStatus
        /// <summary>
        /// Gets the current status of the provided pool instance.
        /// </summary>
        /// <param name="poolHandle">Handle of pool object.</param>
        /// <exception cref="IndyVdrException">Throws if provided pool handle is invalid.</exception>
        /// <returns>Current pool status in json format.</returns>
        public static async Task<string> GetPoolStatusAsync(
            IntPtr poolHandle)
        {
            TaskCompletionSource<string> taskCompletionSource = new();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.indy_vdr_pool_get_status(
                poolHandle,
                PoolGetStatusCallback,
                callbackId
                );

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        private static void PoolGetStatusCallbackMethod(long callbackId, int errorCode, string result)
        {
            TaskCompletionSource<string> taskCompletionSource = PendingCallbacks.Remove<string>(callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(IndyVdrException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(result);
        }
        private static readonly PoolGetStatusCompletedDelegate PoolGetStatusCallback = PoolGetStatusCallbackMethod;
        #endregion

        #region GetPoolTransactions
        /// <summary>
        /// Gets information for all current transactions of a provided pool.</summary>
        /// <param name="poolHandle">Handle of pool object.</param>
        /// <exception cref="IndyVdrException">Throws if provided <paramref name="poolHandle"/> is invalid.</exception>
        /// <returns>Returns a list of all transactions of the pool as json <see cref="string"/>.</returns>
        public static async Task<string> GetPoolTransactionsAsync(
            IntPtr poolHandle)
        {
            TaskCompletionSource<string> taskCompletionSource = new();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.indy_vdr_pool_get_transactions(
                poolHandle,
                PoolGetTransactionsCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            // The return of the native method is not valid json!
            // It only concatentates the transaction objects with a '\n'
            // instead of a ','.
            string transactionsJson = await taskCompletionSource.Task;
            return $"[{transactionsJson.Replace("}\n{", "},{")}]";
        }
        private static void PoolGetTransactionsCallbackMethod(long callbackId, int errorCode, string transactions)
        {
            TaskCompletionSource<string> taskCompletionSource = PendingCallbacks.Remove<string>(callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(IndyVdrException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(transactions);
        }
        private static readonly PoolGetTransactionsCompletedDelegate PoolGetTransactionsCallback = PoolGetTransactionsCallbackMethod;
        #endregion

        #region GetPoolVerifiers
        /// <summary>
        /// Gets information of all current verifiers to a provided pool.
        /// </summary>
        /// <param name="poolHandle">Handle of pool object.</param>
        /// <exception cref="IndyVdrException">Throws if provided <paramref name="poolHandle"/> is invalid.</exception>
        /// <returns>All pool verifiers represented as json <see cref="string"/>.</returns>
        public static async Task<string> GetPoolVerifiersAsync(
            IntPtr poolHandle)
        {
            TaskCompletionSource<string> taskCompletionSource = new();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.indy_vdr_pool_get_verifiers(
                poolHandle,
                PoolGetVerifiersCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }
        private static void PoolGetVerifiersCallbackMethod(long callbackId, int errorCode, string verifiers)
        {
            TaskCompletionSource<string> taskCompletionSource = PendingCallbacks.Remove<string>(callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(IndyVdrException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(verifiers);
        }
        private static readonly PoolGetVerifiersCompletedDelegate PoolGetVerifiersCallback = PoolGetVerifiersCallbackMethod;
        #endregion

        #region SubmitPoolAction
        /// <summary>
        /// Submit a pool action to all verifier nodes.
        ///
        /// The following requests are sent as actions:
        ///    <c>GET_VALIDATOR_INFO</c>,
        ///    <c>POOL_RESTART</c>
        /// </summary>
        /// <param name="poolHandle">Handle of pool object.</param>
        /// <param name="requestHandle">Handle of the prepared request object.</param>
        /// <param name="nodeAliases">All nodes that are requested to perform the action.</param>
        /// <param name="timeout">Seconds until timeout (Default: <c>-1</c> for no timeout).</param>
        /// <exception cref="IndyVdrException">Throws if any parameters are invalid.</exception>
        /// <returns><see cref="string"/> with the node aliases as keys and the node's responses as values within a dictionary in json format.</returns>
        public static async Task<string> SubmitPoolActionAsync(
            IntPtr poolHandle,
            IntPtr requestHandle,
            List<string> nodeAliases = null,
            int timeout = -1)
        {
            string nodesJson = null;
            if (nodeAliases != null && nodeAliases.Any())
            {
                nodesJson = JsonConvert.SerializeObject(nodeAliases);
            }

            TaskCompletionSource<string> taskCompletionSource = new();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.indy_vdr_pool_submit_action(
                poolHandle,
                requestHandle,
                FfiStr.Create(nodesJson),
                timeout,
                PoolSubmitActionCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }
        private static void PoolSubmitActionCallbackMethod(long callbackId, int errorCode, string result)
        {
            TaskCompletionSource<string> taskCompletionSource = PendingCallbacks.Remove<string>(callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(IndyVdrException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(result);
        }
        private static readonly PoolSubmitActionCompletedDelegate PoolSubmitActionCallback = PoolSubmitActionCallbackMethod;
        #endregion

        #region SubmitPoolRequest
        /// <summary>
        /// Submits a ledger request to the pool.
        /// </summary>
        /// <param name="poolHandle">Handle of the pool object.</param>
        /// <param name="requestHandle">Handle of the prepared request object.</param>
        /// <exception cref="IndyVdrException">Throws if <paramref name="poolHandle"/> or <paramref name="requestHandle"/> is invalid.</exception>
        /// <returns>Reply from the pool as json <see cref="string"/>.</returns>
        public static async Task<string> SubmitPoolRequestAsync(
            IntPtr poolHandle,
            IntPtr requestHandle)
        {
            TaskCompletionSource<string> taskCompletionSource = new();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.indy_vdr_pool_submit_request(
                poolHandle,
                requestHandle,
                PoolSubmitRequestCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }
        private static void PoolSubmitRequestCallbackMethod(long callbackId, int errorCode, string result)
        {
            TaskCompletionSource<string> taskCompletionSource = PendingCallbacks.Remove<string>(callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(IndyVdrException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(result);
        }
        private static readonly PoolSubmitRequestCompletedDelegate PoolSubmitRequestCallback = PoolSubmitRequestCallbackMethod;
        #endregion

        /// <summary>
        /// Closes the pool from further actions and frees instance.
        /// </summary>
        /// <param name="poolHandle">Handle of the pool object.</param>
        /// <exception cref="IndyVdrException">Throws if provided <paramref name="poolHandle"/> is invalid.</exception>
        /// <returns>Error code of operation (<c>0</c> if success):</returns>
        public static async Task<int> ClosePoolAsync(
            IntPtr poolHandle)
        {
            int errorCode = NativeMethods.indy_vdr_pool_close(
                poolHandle);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            return errorCode;
        }

    }
}