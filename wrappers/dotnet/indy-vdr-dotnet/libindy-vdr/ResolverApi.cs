using indy_vdr_dotnet;
using indy_vdr_dotnet.libindy_vdr;
using indy_vdr_dotnet.utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static indy_vdr_dotnet.libindy_vdr.NativeMethods;
using static indy_vdr_dotnet.models.Structures;
using static System.Net.Mime.MediaTypeNames;

namespace indy_vdr_dotnet.libindy_vdr
{
     public static class ResolverApi
    {
        public static async Task<string> ResolveAsync(
            IntPtr poolHandle,
            string did)
        {
            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.indy_vdr_resolve(
                await PoolApi.CreatePoolAsync(),
                FfiStr.Create(did),
                ResolveCompletedCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        /// <summary>
        /// Callback method for <see cref="ResolveAsync(IntPtr,string)"/>.
        /// </summary>
        /// <param name="callbackId">The callback id.</param>
        /// <param name="errorCode">Value of the received <see cref="ErrorCode"/> from backend call.</param>
        /// <param name="msg">Value of the received <see cref="msg"/> from backend call.</param>
        private static void ResolveCompleteCallbackMethod(long callbackId, int errorCode, string msg)
        {
            TaskCompletionSource<string> taskCompletionSource = PendingCallbacks.Remove<string>(callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(IndyVdrException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(msg);
        }
        private static readonly ResolveCompletedDelegate ResolveCompletedCallback = ResolveCompleteCallbackMethod;

        public static async Task<string> DereferenceAsync(
           IntPtr poolHandle,
           string did_url)
        {
            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            long callbackId = PendingCallbacks.Add(taskCompletionSource);

            int errorCode = NativeMethods.indy_vdr_dereference(
                await PoolApi.CreatePoolAsync(),
                FfiStr.Create(did_url),
                DereferenceCompletedCallback,
                callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = await ErrorApi.GetCurrentErrorAsync();
                throw IndyVdrException.FromSdkError(error);
            }

            return await taskCompletionSource.Task;
        }

        /// <summary>
        /// Callback method for <see cref="DereferenceAsync(IntPtr,string)"/>.
        /// </summary>
        /// <param name="callbackId">The callback id.</param>
        /// <param name="errorCode">Value of the received <see cref="ErrorCode"/> from backend call.</param>
        /// <param name="msg">Value of the received <see cref="msg"/> from backend call.</param>
        private static void DereferenceCompletedDelegateMethod(long callbackId, int errorCode, string msg)
        {
            TaskCompletionSource<string> taskCompletionSource = PendingCallbacks.Remove<string>(callbackId);

            if (errorCode != (int)ErrorCode.Success)
            {
                string error = ErrorApi.GetCurrentErrorAsync().GetAwaiter().GetResult();
                taskCompletionSource.SetException(IndyVdrException.FromSdkError(error));
                return;
            }
            taskCompletionSource.SetResult(msg);
        }
        private static readonly DereferenceCompletedDelegate DereferenceCompletedCallback = DereferenceCompletedDelegateMethod;
    }

 
       
    
}
