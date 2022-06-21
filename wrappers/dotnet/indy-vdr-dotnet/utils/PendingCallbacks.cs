﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace indy_vdr_dotnet.utils
{/// <summary>
 /// Holder for pending commands.
 /// </summary>
    internal static class PendingCallbacks
    {
        /// <summary>
        /// The next command handle to use.
        /// </summary>
        private static long _nextCallbackId = 0;

        /// <summary>
        /// Gets the next command handle.
        /// </summary>
        /// <returns>The next command handle.</returns>
        public static long GetNextCallbackId()
        {
            return Interlocked.Increment(ref _nextCallbackId);
        }

        /// <summary>
        /// Gets the map of command handles and their task completion sources.
        /// </summary>
        private static IDictionary<long, object> _taskCompletionSources = new ConcurrentDictionary<long, object>();

        /// <summary>
        /// Adds a new TaskCompletionSource to track.
        /// </summary>
        /// <typeparam name="T">The type of the TaskCompletionSource result.</typeparam>
        /// <param name="taskCompletionSource">The TaskCompletionSource to track.</param>
        /// <returns>The command handle to use for tracking the task completion source.</returns>
        public static long Add<T>(TaskCompletionSource<T> taskCompletionSource)
        {
            Debug.Assert(taskCompletionSource != null, "A task completion source is required.");

            long callbackId = GetNextCallbackId();
            _taskCompletionSources.Add(callbackId, taskCompletionSource);
            return callbackId;
        }

        /// <summary>
        /// Gets and removes a TaskCompletionResult from tracking.
        /// </summary>
        /// <typeparam name="T">The type of the TaskCompletionResult that was tracked.</typeparam>
        /// <param name="commandHandle">The command handle used for tracking the TaskCompletionResult.</param>
        /// <returns>The TaskCompletionResult associated with the command handle.</returns>
        public static TaskCompletionSource<T> Remove<T>(long callbackId)
        {
            Debug.Assert(_taskCompletionSources.ContainsKey(callbackId), string.Format("No task completion source is currently registered for the command with the handle '{0}'.", callbackId));

            var taskCompletionSource = _taskCompletionSources[callbackId];
            _taskCompletionSources.Remove(callbackId);
            var result = taskCompletionSource as TaskCompletionSource<T>;

            Debug.Assert(result != null, string.Format("No  task completion source of the specified type is registered for the command with the handle '{0}'.", callbackId));

            return result;
        }
    }
}
