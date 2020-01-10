use std::collections::HashMap;

use serde_json;
use serde_json::Value as SJsonValue;

use super::types::{NodeTransaction, NodeTransactionV0, NodeTransactionV1, TransactionMap};
use crate::domain::pool::ProtocolVersion;
use crate::utils::merkletree::MerkleTree;
// use crate::utils::environment;
use crate::utils::error::prelude::*;

// use byteorder::{LittleEndian, ReadBytesExt, WriteBytesExt};

const POOL_EXT: &str = "txn";

/*pub fn create(pool_name: &str) -> LedgerResult<MerkleTree> {
    let mut p = environment::pool_path(pool_name);

    let mut p_stored = p.clone();
    p_stored.push("stored");
    p_stored.set_extension("btxn");

    if !p_stored.exists() {
        trace!("Restoring merkle tree from genesis");
        p.push(pool_name);
        p.set_extension(POOL_EXT);

        if !p.exists() {
            trace!("here");
            return Err(err_msg(
                LedgerErrorKind::PoolNotCreated,
                format!("Pool is not created for name: {:?}", pool_name),
            ));
        }

        _from_genesis(&p)
    } else {
        trace!("Restoring merkle tree from cache");
        _from_cache(&p_stored)
    }
}

pub fn drop_cache(pool_name: &str) -> LedgerResult<()> {
    let p = get_pool_stored_path(pool_name, false);
    if p.exists() {
        warn!("Cache is invalid -- dropping it!");
        fs::remove_file(p).to_result(
            LedgerErrorKind::IOError,
            "Can't drop pool ledger cache file",
        )?;
        Ok(())
    } else {
        Err(err_msg(
            LedgerErrorKind::InvalidState,
            "Can't recover to genesis -- no txns stored. Possible problems in genesis txns.",
        ))
    }
}

pub fn from_cache(file_name: &PathBuf) -> LedgerResult<MerkleTree> {
    let mut mt = MerkleTree::from_vec(Vec::new())?;

    let mut f = fs::File::open(file_name).to_result(
        LedgerErrorKind::IOError,
        "Can't open pool ledger cache file",
    )?;

    trace!("Start recover from cache");

    loop {
        let bytes = match f.read_u64::<LittleEndian>() {
            Ok(bytes) => bytes,
            Err(ref e) if e.kind() == io::ErrorKind::UnexpectedEof => break,
            Err(e) => {
                return Err(e.to_result(
                    LedgerErrorKind::IOError,
                    "Can't read from pool ledger cache file",
                ))
            }
        };

        trace!("bytes: {:?}", bytes);
        let mut buf = vec![0; bytes as usize];

        match f.read_exact(buf.as_mut()) {
            Ok(()) => (),
            Err(e) => match e.kind() {
                io::ErrorKind::UnexpectedEof => {
                    return Err(e.to_result(
                        LedgerErrorKind::InvalidState,
                        "Malformed pool ledger cache file",
                    ))
                }
                _ => {
                    return Err(e.to_result(
                        LedgerErrorKind::IOError,
                        "Can't read from pool ledger cache file",
                    ))
                }
            },
        }

        mt.append(buf.to_vec())?;
    }

    Ok(mt)
}*/

/*
pub fn from_genesis(file_name: &PathBuf) -> LedgerResult<MerkleTree> {
    let mut mt = MerkleTree::from_vec(Vec::new())?;

    let f = fs::File::open(file_name)
        .to_result(LedgerErrorKind::IOError, "Can't open genesis txn file")?;

    let reader = io::BufReader::new(&f);

    for line in reader.lines() {
        let line: String =
            line.to_result(LedgerErrorKind::IOError, "Can't read from genesis txn file")?;

        if line.trim().is_empty() {
            continue;
        };
        mt.append(_parse_txn_from_json(&line)?)?;
    }

    Ok(mt)
}

pub fn from_genesis_file(txn_file: &str) -> LedgerResult<MerkleTree> {
    from_genesis(&PathBuf::from(txn_file))
}
*/

/*
fn get_pool_stored_path(pool_name: &str, create_dir: bool) -> PathBuf {
    get_pool_stored_path_base(pool_name, create_dir, "stored", "btxn")
}

fn get_pool_stored_path_base(
    pool_name: &str,
    create_dir: bool,
    filename: &str,
    ext: &str,
) -> PathBuf {
    let mut path = environment::pool_path(pool_name);
    if create_dir {
        fs::create_dir_all(path.as_path()).unwrap();
    }
    path.push(filename);
    path.set_extension(ext);
    path
}*/

/*
pub fn dump_new_txns(pool_name: &str, txns: &[Vec<u8>]) -> LedgerResult<()> {
    let p = get_pool_stored_path(pool_name, false);
    if !p.exists() {
        _dump_genesis_to_stored(&p, pool_name)?;
    }

    let mut file = fs::OpenOptions::new().append(true).open(p).to_result(
        LedgerErrorKind::IOError,
        "Can't open pool ledger cache file",
    )?;

    _dump_vec_to_file(txns, &mut file)
}

pub fn _dump_genesis_to_stored(p: &PathBuf, pool_name: &str) -> LedgerResult<()> {
    let p_genesis = get_pool_stored_path_base(pool_name, false, pool_name, POOL_EXT);

    if !p_genesis.exists() {
        trace!("here");
        return Err(err_msg(
            LedgerErrorKind::PoolNotCreated,
            format!("Pool is not created for name: {:?}", pool_name),
        ));
    }

    let mut file = fs::File::create(p).to_result(
        LedgerErrorKind::IOError,
        "Can't create pool ledger cache file",
    )?;

    let genesis_vec = _genesis_to_binary(&p_genesis)?;
    _dump_vec_to_file(&genesis_vec, &mut file)
}*/

/*
fn _dump_vec_to_file(v: &[Vec<u8>], file: &mut fs::File) -> LedgerResult<()> {
    for ref line in v {
        file.write_u64::<LittleEndian>(line.len() as u64)
            .to_result(
                LedgerErrorKind::IOError,
                "Can't write to pool ledger cache file",
            )?;

        file.write_all(line).to_result(
            LedgerErrorKind::IOError,
            "Can't write to pool ledger cache file",
        )?;
    }

    Ok(())
}
*/

/*
pub fn load_genesis_txns(p: &PathBuf) -> LedgerResult<JsonTransactions> {
    let f = fs::File::open(p).to_result(LedgerErrorKind::IOError, "Can't open genesis txn file")?;

    let reader = io::BufReader::new(&f);
    let mut txns: Vec<String> = vec![];

    for line in reader.lines() {
        let line = line.to_result(LedgerErrorKind::IOError, "Can't read from genesis txn file")?;
        if !line.is_empty() {
            txns.push(line);
        }
    }
    Ok(JsonTransactions::new(txns))
}
*/

pub fn build_tree(json_tnxs: &Vec<String>) -> LedgerResult<MerkleTree> {
    let mut bin_txns: Vec<Vec<u8>> = vec![];
    for json_txn in json_tnxs {
        let bin_txn = _parse_txn_from_json(json_txn)?;
        bin_txns.push(bin_txn)
    }
    MerkleTree::from_vec(bin_txns)
}

fn _parse_txn_from_json(txn: &str) -> LedgerResult<Vec<u8>> {
    let txn = txn.trim();

    if txn.is_empty() {
        return Ok(vec![]);
    }

    let txn: SJsonValue = serde_json::from_str(txn).to_result(
        LedgerErrorKind::InvalidStructure,
        "Genesis txn is mailformed json",
    )?;

    rmp_serde::encode::to_vec_named(&txn).to_result(
        LedgerErrorKind::InvalidState,
        "Can't encode genesis txn as message pack",
    )
}

fn _decode_transaction(
    gen_txn: &Vec<u8>,
    protocol_version: ProtocolVersion,
) -> LedgerResult<NodeTransactionV1> {
    let gen_txn: NodeTransaction = rmp_serde::decode::from_slice(gen_txn.as_slice()).to_result(
        LedgerErrorKind::InvalidState,
        "Genesis transaction cannot be decoded",
    )?;

    match gen_txn {
        NodeTransaction::NodeTransactionV0(txn) => {
            if protocol_version != 1 {
                Err(err_msg(LedgerErrorKind::PoolIncompatibleProtocolVersion,
                            format!("Libindy PROTOCOL_VERSION is {} but Pool Genesis Transactions are of version {}.\
                                    Call indy_set_protocol_version(1) to set correct PROTOCOL_VERSION",
                                    protocol_version, NodeTransactionV0::VERSION)))
            } else {
                Ok(NodeTransactionV1::from(txn))
            }
        }
        NodeTransaction::NodeTransactionV1(txn) => {
            if protocol_version != 2 {
                Err(err_msg(LedgerErrorKind::PoolIncompatibleProtocolVersion,
                                format!("Libindy PROTOCOL_VERSION is {} but Pool Genesis Transactions are of version {}.\
                                            Call indy_set_protocol_version(2) to set correct PROTOCOL_VERSION",
                                        protocol_version, NodeTransactionV1::VERSION)))
            } else {
                Ok(txn)
            }
        }
    }
}

pub fn build_node_state_from_tree(
    merkle_tree: &MerkleTree,
    protocol_version: ProtocolVersion,
) -> LedgerResult<TransactionMap> {
    let mut gen_tnxs: TransactionMap = HashMap::new();

    for gen_txn in merkle_tree {
        let mut node_txn = _decode_transaction(gen_txn, protocol_version)?;
        if gen_tnxs.contains_key(&node_txn.txn.data.dest) {
            gen_tnxs
                .get_mut(&node_txn.txn.data.dest)
                .unwrap()
                .update(&mut node_txn)?;
        } else {
            gen_tnxs.insert(node_txn.txn.data.dest.clone(), node_txn);
        }
    }
    Ok(gen_tnxs)
}

pub fn build_node_state_from_json(
    json_tnxs: Vec<String>,
    protocol_version: ProtocolVersion,
) -> LedgerResult<TransactionMap> {
    let mut gen_tnxs: TransactionMap = HashMap::new();

    for gen_txn in json_tnxs {
        let pack_txn = _parse_txn_from_json(&gen_txn)?;
        let mut node_txn = _decode_transaction(&pack_txn, protocol_version)?;
        if gen_tnxs.contains_key(&node_txn.txn.data.dest) {
            gen_tnxs
                .get_mut(&node_txn.txn.data.dest)
                .unwrap()
                .update(&mut node_txn)?;
        } else {
            gen_tnxs.insert(node_txn.txn.data.dest.clone(), node_txn);
        }
    }
    Ok(gen_tnxs)
}

/*
FIXME

#[cfg(test)]
mod tests {
    use std::fs;

    use byteorder::LittleEndian;

    use crate::domain::pool::ProtocolVersion;
    use crate::utils::test;

    use super::*;

    fn _set_protocol_version(version: usize) {
        ProtocolVersion::set(version);
    }

    const TEST_PROTOCOL_VERSION: usize = 2;
    pub const NODE1_OLD: &str = r#"{"data":{"alias":"Node1","client_ip":"192.168.1.35","client_port":9702,"node_ip":"192.168.1.35","node_port":9701,"services":["VALIDATOR"]},"dest":"Gw6pDLhcBcoQesN72qfotTgFa7cbuqZpkX3Xo6pLhPhv","identifier":"FYmoFw55GeQH7SRFa37dkx1d2dZ3zUF8ckg7wmL7ofN4","txnId":"fea82e10e894419fe2bea7d96296a6d46f50f93f9eeda954ec461b2ed2950b62","type":"0"}"#;
    pub const NODE2_OLD: &str = r#"{"data":{"alias":"Node2","client_ip":"192.168.1.35","client_port":9704,"node_ip":"192.168.1.35","node_port":9703,"services":["VALIDATOR"]},"dest":"8ECVSk179mjsjKRLWiQtssMLgp6EPhWXtaYyStWPSGAb","identifier":"8QhFxKxyaFsJy4CyxeYX34dFH8oWqyBv1P4HLQCsoeLy","txnId":"1ac8aece2a18ced660fef8694b61aac3af08ba875ce3026a160acbc3a3af35fc","type":"0"}"#;

    fn _write_genesis_txns(pool_name: &str, txns: &str) {
        let path = get_pool_stored_path_base(pool_name, true, pool_name, POOL_EXT);
        let mut f = fs::File::create(path.as_path()).unwrap();
        f.write(txns.as_bytes()).unwrap();
        f.flush().unwrap();
        f.sync_all().unwrap();
    }

    #[test]
    fn pool_worker_build_node_state_works_for_new_txns_format_and_1_protocol_version() {
        test::cleanup_storage(
            "pool_worker_build_node_state_works_for_new_txns_format_and_1_protocol_version",
        );

        _set_protocol_version(1);

        let node_txns = test::gen_txns();
        let txns_src = node_txns[0..(2 as usize)].join("\n");

        _write_genesis_txns(
            "pool_worker_build_node_state_works_for_new_txns_format_and_1_protocol_version",
            &txns_src,
        );

        let merkle_tree = super::create(
            "pool_worker_build_node_state_works_for_new_txns_format_and_1_protocol_version",
        )
        .unwrap();
        let res = super::build_node_state(&merkle_tree);
        assert_kind!(LedgerErrorKind::PoolIncompatibleProtocolVersion, res);

        test::cleanup_storage(
            "pool_worker_build_node_state_works_for_new_txns_format_and_1_protocol_version",
        );
    }

    #[test]
    pub fn pool_worker_works_for_deserialize_cache() {
        test::cleanup_storage("pool_worker_works_for_deserialize_cache");
        {
            _set_protocol_version(TEST_PROTOCOL_VERSION);

            let node_txns = test::gen_txns();

            let txn1_json: serde_json::Value = serde_json::from_str(&node_txns[0]).unwrap();
            let txn2_json: serde_json::Value = serde_json::from_str(&node_txns[1]).unwrap();
            let txn3_json: serde_json::Value = serde_json::from_str(&node_txns[2]).unwrap();
            let txn4_json: serde_json::Value = serde_json::from_str(&node_txns[3]).unwrap();

            let pool_cache = vec![
                rmp_serde::to_vec_named(&txn1_json).unwrap(),
                rmp_serde::to_vec_named(&txn2_json).unwrap(),
                rmp_serde::to_vec_named(&txn3_json).unwrap(),
                rmp_serde::to_vec_named(&txn4_json).unwrap(),
            ];

            let pool_name = "pool_worker_works_for_deserialize_cache";
            let path = get_pool_stored_path(pool_name, true);
            let mut f = fs::File::create(path.as_path()).unwrap();
            pool_cache.iter().for_each(|vec| {
                f.write_u64::<LittleEndian>(vec.len() as u64).unwrap();
                f.write_all(vec).unwrap();
            });

            let merkle_tree = super::create(pool_name).unwrap();
            let _node_state = super::build_node_state(&merkle_tree).unwrap();
        }
        test::cleanup_storage("pool_worker_works_for_deserialize_cache");
    }

    #[test]
    fn pool_worker_restore_merkle_tree_works_from_genesis_txns() {
        test::cleanup_storage("pool_worker_restore_merkle_tree_works_from_genesis_txns");

        let node_txns = test::gen_txns();
        let txns_src = format!(
            "{}\n{}",
            node_txns[0].replace(environment::test_pool_ip().as_str(), "10.0.0.2"),
            node_txns[1].replace(environment::test_pool_ip().as_str(), "10.0.0.2")
        );
        _write_genesis_txns(
            "pool_worker_restore_merkle_tree_works_from_genesis_txns",
            &txns_src,
        );

        let merkle_tree =
            super::create("pool_worker_restore_merkle_tree_works_from_genesis_txns").unwrap();

        assert_eq!(merkle_tree.count(), 2, "test restored MT size");
        assert_eq!(
            merkle_tree.root_hash_hex(),
            "c715aef44aaacab8746c9a505ba106b5554fe6d29ec7f0a2abc9d7723fdea523",
            "test restored MT root hash"
        );

        test::cleanup_storage("pool_worker_restore_merkle_tree_works_from_genesis_txns");
    }

    #[test]
    fn pool_worker_build_node_state_works_for_old_format() {
        test::cleanup_storage("pool_worker_build_node_state_works_for_old_format");

        _set_protocol_version(1);

        let node1: NodeTransactionV1 =
            NodeTransactionV1::from(serde_json::from_str::<NodeTransactionV0>(NODE1_OLD).unwrap());
        let node2: NodeTransactionV1 =
            NodeTransactionV1::from(serde_json::from_str::<NodeTransactionV0>(NODE2_OLD).unwrap());

        let txns_src = format!("{}\n{}\n", NODE1_OLD, NODE2_OLD);

        _write_genesis_txns(
            "pool_worker_build_node_state_works_for_old_format",
            &txns_src,
        );

        let merkle_tree =
            super::create("pool_worker_build_node_state_works_for_old_format").unwrap();
        let node_state = super::build_node_state(&merkle_tree).unwrap();

        assert_eq!(1, ProtocolVersion::get());

        assert_eq!(2, node_state.len());
        assert!(node_state.contains_key("Gw6pDLhcBcoQesN72qfotTgFa7cbuqZpkX3Xo6pLhPhv"));
        assert!(node_state.contains_key("8ECVSk179mjsjKRLWiQtssMLgp6EPhWXtaYyStWPSGAb"));

        assert_eq!(
            node_state["Gw6pDLhcBcoQesN72qfotTgFa7cbuqZpkX3Xo6pLhPhv"],
            node1
        );
        assert_eq!(
            node_state["8ECVSk179mjsjKRLWiQtssMLgp6EPhWXtaYyStWPSGAb"],
            node2
        );

        test::cleanup_storage("pool_worker_build_node_state_works_for_old_format");
    }

    #[test]
    fn pool_worker_build_node_state_works_for_new_format() {
        test::cleanup_storage("pool_worker_build_node_state_works_for_new_format");

        _set_protocol_version(TEST_PROTOCOL_VERSION);

        let node_txns = test::gen_txns();

        let node1: NodeTransactionV1 = serde_json::from_str(&node_txns[0]).unwrap();
        let node2: NodeTransactionV1 = serde_json::from_str(&node_txns[1]).unwrap();

        let txns_src = node_txns.join("\n");

        _write_genesis_txns(
            "pool_worker_build_node_state_works_for_new_format",
            &txns_src,
        );

        let merkle_tree =
            super::create("pool_worker_build_node_state_works_for_new_format").unwrap();
        let node_state = super::build_node_state(&merkle_tree).unwrap();

        assert_eq!(2, ProtocolVersion::get());

        assert_eq!(4, node_state.len());
        assert!(node_state.contains_key("Gw6pDLhcBcoQesN72qfotTgFa7cbuqZpkX3Xo6pLhPhv"));
        assert!(node_state.contains_key("8ECVSk179mjsjKRLWiQtssMLgp6EPhWXtaYyStWPSGAb"));

        assert_eq!(
            node_state["Gw6pDLhcBcoQesN72qfotTgFa7cbuqZpkX3Xo6pLhPhv"],
            node1
        );
        assert_eq!(
            node_state["8ECVSk179mjsjKRLWiQtssMLgp6EPhWXtaYyStWPSGAb"],
            node2
        );

        test::cleanup_storage("pool_worker_build_node_state_works_for_new_format");
    }

    #[test]
    fn pool_worker_build_node_state_works_for_old_txns_format_and_2_protocol_version() {
        test::cleanup_storage(
            "pool_worker_build_node_state_works_for_old_txns_format_and_2_protocol_version",
        );

        _set_protocol_version(TEST_PROTOCOL_VERSION);

        let txns_src = format!("{}\n{}\n", NODE1_OLD, NODE2_OLD);

        _write_genesis_txns(
            "pool_worker_build_node_state_works_for_old_txns_format_and_2_protocol_version",
            &txns_src,
        );

        let merkle_tree = super::create(
            "pool_worker_build_node_state_works_for_old_txns_format_and_2_protocol_version",
        )
        .unwrap();
        let res = super::build_node_state(&merkle_tree);
        assert_kind!(LedgerErrorKind::PoolIncompatibleProtocolVersion, res);

        test::cleanup_storage(
            "pool_worker_build_node_state_works_for_old_txns_format_and_2_protocol_version",
        );
    }
}
*/
