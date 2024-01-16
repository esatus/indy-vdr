using System;
using System.Runtime.InteropServices;
using static indy_vdr_dotnet.models.Structures;

namespace indy_vdr_dotnet.libindy_vdr
{
    internal static class NativeMethods
    {
        #region Error
        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_get_current_error(ref string error_json_p);
        #endregion

        #region Ledger
        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_acceptance_mechanisms_request(FfiStr submitter_did, FfiStr aml, FfiStr version, FfiStr aml_context, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_acceptance_mechanisms_request(FfiStr submitter_did, long timestamp, FfiStr version, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_attrib_request(FfiStr submitter_did, FfiStr target_did, FfiStr hash, FfiStr raw, FfiStr enc, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_attrib_request(FfiStr submitter_did, FfiStr target_did, FfiStr raw, FfiStr hash, FfiStr enc, int seq_no, long timestamp, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_cred_def_request(FfiStr submitter_did, FfiStr cred_def, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_custom_request(FfiStr request_json, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_disable_all_txn_author_agreements_request(FfiStr submitter_did, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_cred_def_request(FfiStr submitter_did, FfiStr cred_def_id, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_nym_request(FfiStr submitter_did, FfiStr dest, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_revoc_reg_def_request(FfiStr submitter_did, FfiStr revoc_reg_id, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_revoc_reg_request(FfiStr submitter_did, FfiStr revoc_reg_id, long timestamp, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_revoc_reg_delta_request(FfiStr submitter_did, FfiStr revoc_reg_id, long from_ts, long to_ts, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_schema_request(FfiStr submitter_did, FfiStr schema_id, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_txn_author_agreement_request(FfiStr submitter_did, FfiStr data, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_txn_request(FfiStr submitter_did, int ledger_type, int seq_no, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_get_validator_info_request(FfiStr submitter_did, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_nym_request(FfiStr submitter_did, FfiStr dest, FfiStr verkey, FfiStr alias, FfiStr role, FfiStr diddoc_content, int version, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_revoc_reg_def_request(FfiStr submitter_did, FfiStr revoc_reg_def, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_revoc_reg_entry_request(FfiStr submitter_did, FfiStr revoc_reg_def_id, FfiStr revoc_reg_def_type, FfiStr revoc_reg_entry, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_schema_request(FfiStr submitter_did, FfiStr schema, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_build_txn_author_agreement_request(FfiStr submitter_did, FfiStr text, FfiStr version, long ratification_ts, long retirement_ts, ref IntPtr handle_p);
        #endregion

        #region Mod
        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_set_config(FfiStr config);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_set_default_logger();

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_set_protocol_version(long version);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_set_socks_proxy(FfiStr socks_proxy);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern string indy_vdr_version();
        #endregion

        #region Pool
        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_pool_create(FfiStr param, ref IntPtr handle_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_pool_refresh(IntPtr pool_handle, PoolRefreshCompletedDelegate callback, long callback_id);
        internal delegate void PoolRefreshCompletedDelegate(long callback_id, int err);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_pool_get_status(IntPtr pool_handle, PoolGetStatusCompletedDelegate callback, long callback_id);
        internal delegate void PoolGetStatusCompletedDelegate(long callback_id, int err, string response);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_pool_get_transactions(IntPtr pool_handle, PoolGetTransactionsCompletedDelegate callback, long callback_id);
        internal delegate void PoolGetTransactionsCompletedDelegate(long callback_id, int err, string response);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_pool_get_verifiers(IntPtr pool_handle, PoolGetVerifiersCompletedDelegate callback, long callback_id);
        internal delegate void PoolGetVerifiersCompletedDelegate(long callback_id, int err, string response);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_pool_submit_action(IntPtr pool_handle, IntPtr request_handle, FfiStr nodes, int timeout, PoolSubmitActionCompletedDelegate callback, long callback_id);
        internal delegate void PoolSubmitActionCompletedDelegate(long callback_id, int err, string response);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_pool_submit_request(IntPtr pool_handle, IntPtr request_handle, PoolSubmitRequestCompletedDelegate callback, long callback_id);
        internal delegate void PoolSubmitRequestCompletedDelegate(long callback_id, int err, string response);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_pool_close(IntPtr pool_handle);
        #endregion

        #region Request
        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_prepare_txn_author_agreement_acceptance(FfiStr text, FfiStr version, FfiStr taa_digest, FfiStr acc_mech_type, ulong time, ref string output_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_request_free(IntPtr request_handle);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_request_get_body(IntPtr request_handle, ref string body_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_request_get_signature_input(IntPtr request_handle, ref string input_p);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_request_set_endorser(IntPtr request_handle, FfiStr endorser);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_request_set_multi_signature(IntPtr request_handle, FfiStr identifier, ByteBuffer signature);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_request_set_signature(IntPtr request_handle, ByteBuffer signature);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_request_set_txn_author_agreement_acceptance(IntPtr request_handle, FfiStr acceptance);
        #endregion

     
        #region Resolve
        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_resolve(IntPtr pool_handle, FfiStr did, ResolveCompletedDelegate callback, long callback_id);
        internal delegate void ResolveCompletedDelegate(long callback_id, int err, string response);

        [DllImport(Consts.LIBINDY_VDR_NAME, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern int indy_vdr_dereference(IntPtr pool_handle, FfiStr did, DereferenceCompletedDelegate cb, long callback_id);
        internal delegate void DereferenceCompletedDelegate(long callback_id, int err, string response);
        #endregion
    }
}