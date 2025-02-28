using System.Collections.Generic;
using System.Numerics;
using SeguraChain_Lib.Blockchain.Mining.Object;
using SeguraChain_Lib.Blockchain.Setting.Function;
using SeguraChain_Lib.Blockchain.Sovereign.Database;
using SeguraChain_Lib.Utility;

namespace SeguraChain_Lib.Blockchain.Setting
{

    public class BlockchainSetting
    {
        #region Blockchain Settings & Properties.

        /// <summary>
        /// Wallet Settings.
        /// </summary>
        public const int WalletAddressWifLengthMin = 109;
        public const int WalletAddressWifLengthMax = 110;
        public const int WalletPublicKeyWifLength = 219;
        public const int WalletPrivateKeyWifLength = 119;

        /// <summary>
        /// Computations settings for wallet generator.
        /// </summary>
        public const int WalletPublicKeyByteArrayLength = 64;
        public const int WalletPrivateKeyWifByteArrayLength = 71;
        public const int WalletAddressByteArrayLength = 65;

        /// <summary>
        /// Signature Settings.
        /// </summary>
        public const string CurveName = "sect571r1";
        public const string SignerName = "SHA512WITHECDSA";
        public const string SignerNameNetwork = "SHA256WITHECDSA";
        public const string CurveNameBitcoin = "secp256k1";

        /// <summary>
        /// Transaction Settings.
        /// </summary>
        public const int TransactionVersion = 1;
        public const int TransactionMandatoryBlockRewardConfirmations = 10;
        public const int TransactionMandatoryMinBlockTransactionConfirmations = 2; // The minimum mandatory confirmations accepted on transactions.
        public const int TransactionMandatoryMaxBlockTransactionConfirmationsDays = 7;
        public const int TransactionMandatoryMaxBlockTransactionConfirmations = BlockExpectedPerDay * TransactionMandatoryMaxBlockTransactionConfirmationsDays; // The maximum mandatory confirmations accepted on transactions. Max amount of confirmations is around the amount of blocks mined in 7 days.
        public const int TransactionMandatoryMinBlockHeightStartConfirmation = 5; // The minimum mandatory block height start to target for a transaction.
        public const int TransactionMandatoryMaxBlockHeightStartConfirmation = BlockExpectedPerDay; // The maximum mandatory block height start to target for a transaction.
        public const int TransactionInvalidDelayRemove = 86_400; // Remove a transaction set has invalid after a delay of 86400 seconds.
        public const int TransactionHashSize = (sizeof(long) * 2) + 128; // block height long hex size + sha512 hex size size.

        /// <summary>
        /// Coin Settings.
        /// </summary>
        public const long CoinDecimal = 100_000_000;
        public const int CoinDecimalNumber = 8;
        public const long MinFeeTransaction = CoinDecimal / 500_000;
        public const long FeeTransactionPerKb = CoinDecimal / 500_000;
        public const long MinAmountTransaction = 1;

        /// <summary>
        /// Blockchain Settings.
        /// </summary>
        public const int MaxTransactionPerBlock = 300_000;
        public const int BlockRewardHalvingRange = 100_000;
        public static readonly BigInteger BlockRewardStatic = (10 * CoinDecimal);
        public const decimal BlockDevFeePercent = 0.005m;
        public static readonly BigInteger MaxSupply = 26_000_000 * CoinDecimal;

        /// <summary>
        /// Task transaction confirmation settings.
        /// </summary>
        public const int TaskVirtualWalletBalanceCheckpoint = 20; // Speed up tx confirmation process by save wallet balance calculated checkpoints.
        public const int TaskVirtualTransactionCheckpoint = 20; // Do not check again transactions signatures, data after 20 valid confirmations done.
        public const int TaskVirtualBlockCheckpoint = 50; // Increase speed of checking block transactions.

        #region Dev wallet Information settings.

        /// <summary>
        /// Sovereign Update Settings.
        /// </summary>
        public static string WalletAddressDevPublicKey(long timestampSovereignUpdate) => SovereignUpdateGetter.GetLastDevWalletPublicKey(timestampSovereignUpdate);
        public static string WalletAddressDev(long timestampSovereignUpdate) => SovereignUpdateGetter.GetLastDevWalletAddress(timestampSovereignUpdate);

		public const string DefaultWalletAddressDevPublicKey ="YA87vT4aMGckKE54yLpejNC8XqPd2iYXca7nywmYnJhT1xRicBSMERPjLbyHg3SncReq2e54jXZqSbe5t5ekq2jAWmZMMPHquqvcKZNdcDSVBCdQwBL8E5nAqJUPppLVB9F5xQi2JF1MMRsZkQhvhMfi4K9q78QRDpgEqoRHqjiPVQiD52DDkzMC4pvLLtbzZG3cr6j3YVYrWYnYQmuCSxwmtm1";
		public const string DefaultWalletAddressDev ="3wYidzXerqWukK1cUw9JkpGcKtw1WQZbKNYMBStb2jEmbnc5n7qCGEdC6odWosWMVYA8HdfMioaDqnEQDKx2dTXEGaEGegxfiX9hnbGG6VaboT";

        #endregion

        /// <summary>
        /// Take in count halving.
        /// </summary>
        /// <param name="blockHeight">The block height for calculate halving.</param>
        /// <returns></returns>
        public static BigInteger BlockReward(long blockHeight) => ClassBlockRewardFunction.GetBlockRewardWithHalving(blockHeight);
        public static BigInteger BlockDevFee(long blockHeight) => ClassBlockRewardFunction.GetDevFeeWithHalving(blockHeight);
        public static BigInteger BlockRewardWithDevFee(long blockHeight) => (BlockReward(blockHeight) - BlockDevFee(blockHeight));


        public const string BlockRewardName = "BLOCK"; // Block reward transaction name.
        public const int BlockTime = 60; // The block time scheduled in seconds.
        public const int BlockExpectedPerDay = (int)((24 * 60d) / (BlockTime / 60d)); // The average of blocks calculated from the scheduled Block Time and the amount of minutes in a single day.
        public const int BlockDifficultyRangeCalculation = BlockExpectedPerDay / 3; // Use previous blocks for generate the new block difficulty.
        public const int BlockDifficultyPrecision = 100_000; // The precision put on the difficulty factor calculated.
        public const int GenesisBlockHeight = 1; // The genesis block height.
        public static readonly BigInteger GenesisBlockAmount = 2_973_370 * CoinDecimal; // The genesis block amount reward has pre-mining.
		public const string GenesisBlockFinalTransactionHash ="DFC39F909B0C1F7F1228BBA1F1F4C9E6F3933A378C569812C095C42DF527164C6875208BF4D645ECC489257FF79BF989FABDC05720364C99EB8E621035EA2BEF";
        public const int GenesisBlockTransactionCount = 1; // The maximum of transaction inserted on the genesis block.
        public const int BlockAmountNetworkConfirmations = 2; // The minimum amount of network checks on blocks to do with peers, before to enable the task of confirmations on the block.
        public const int BlockAmountSlowNetworkConfirmations = 5; // This amount increment another amount of network checks, once this one is reach, the network counter increment and this one return back to 0.
        public const int BlockMiningUnlockShareTimestampMaxDelay = BlockTime; // The maximum of time allowed on a mining share timestamp received. The broadcasting of this one need to be enough fast to reach the majority of nodes.
        public const int BlockSyncAmountNetworkConfirmationsCheckpointPassed = 10; // If the block synced height is below the latest block height unlocked, the block synced network confirmations is automatically filled.

        /// <summary>
        /// Blockchain Properties.
        /// </summary>
        public const string CoinName = "XENOPHYTE";
        public const string CoinMinName = "XENOP";
        public const string BlockchainVersion = "01"; // Version of the blockchain used on Base58.
        public static readonly byte[] BlockchainVersionByteArray = ClassUtility.GetByteArrayFromHexString(BlockchainVersion);
        public const int BlockchainChecksum = 16; // Checksum size used on Base58.

        /// <summary>
        /// Used on some parts of the code: Encryption, network and more..
        /// </summary>
        public static readonly byte[] BlockchainMarkKey = ClassUtility.GetByteArrayFromStringAscii(ClassUtility.GenerateSha3512FromString(new byte[] { 0x73, 0x61, 0x6d, 0x20, 0x73, 0x65, 0x67, 0x75, 0x72, 0x61 }.GetStringFromByteArrayAscii()));
        public const int BlockchainSha512HexStringLength = 128;
        public const int BlockchainSha256HexStringLength = 64;

        /// <summary>
        /// Block hash description size.
        /// </summary>
        public const int BlockHeightByteArrayLengthOnBlockHash = sizeof(long);
        public const int BlockDifficultyByteArrayLengthOnBlockHash = sizeof(double);
        public const int BlockCountTransactionByteArrayLengthOnBlockHash = sizeof(int);
        public const int BlockFinalTransactionHashByteArrayLengthOnBlockHash = 64; // SHA512
        public const int BlockHashByteArraySize = BlockHeightByteArrayLengthOnBlockHash + BlockDifficultyByteArrayLengthOnBlockHash + BlockCountTransactionByteArrayLengthOnBlockHash + BlockFinalTransactionHashByteArrayLengthOnBlockHash + WalletAddressByteArrayLength;
        public const int BlockHashHexSize = BlockHashByteArraySize * 2;

        /// <summary>
        /// Average of mining luck calculated from block range mined.
        /// </summary>
        public const double BlockMiningStatsAvgPoorLuck = 0.85d;
        public const double BlockMiningStatsAvgNormalLuck = 1d;
        public const double BlockMiningStatsAvgLucky = 1.15d;
        public const double BlockMiningStatsAvgVeryLucky = 1.30d;
        public const double BlockMiningStatsAvgWarningLuck = 1.5d;

        #endregion

        #region Peer P2P Settings & Properties.

        /// <summary>
        /// The peer unique id hash, permit to support multiple peers with different id hash from the same hostname/ip.
        /// </summary>
        public const int PeerUniqueIdHashLength = 128;
        public const int PeerIvIterationCount = 1024;

        /// <summary>
        /// Peer P2P Settings can be changed on the node setting file.
        /// </summary>
        public const int PeerMaxNoPacketConnectionAttempt = 40;
        public const int PeerMaxInvalidPacket = 60; // Banned after 60 invalid packets.
        public const int PeerMaxDelayAwaitResponse = 60; // Await a response from a peer target pending maximum 30 seconds per requests sent.
        public const int PeerMaxDelayConnection = 30; // A maximum of 30 seconds on receive a packet.
        public const int PeerMaxTimestampDelayPacket = 360; // Await a maximum of 360 seconds on the timestamp of a packet, above the packet is considered has expired.
        public const int PeerMaxDelayKeepAliveStats = 60; // Keep alive packet stats of a peer pending 60 seconds.
        public const int PeerMaxEarlierPacketDelay = 600; // A maximum of 600 seconds is accepted on timestamp of packets.
        public const int PeerMaxDelayToConnectToTarget = 10; // A maximum of 10 seconds delay on connect to a peer.
        public const int PeerMaxAttemptConnection = 20; // After 20 retries to connect to a peer, the peer target is set has dead pending a certain amount of time.
        public const int PeerBanDelay = 30; // Ban delay pending 30 seconds.
        public const int PeerDeadDelay = 15; // Dead delay pending 15 seconds.
        public const int PeerMinValidPacket = 2; // Do not check packet signature after 2 valid packets sent.
        public const int PeerMaxWhiteListPacket = 1000; // Empty valid packet counter of a peer after to have ignoring packet signature 1000 of a peer.
        public const int PeerTaskSyncDelay = 10;
        public const int MaxPeerPerSyncTask = 50;
        public const int PeerMinAvailablePeerSync = 1; // The minimum of required peer(s).
        public const int PeerMaxAuthKeysExpire = 86_400 * 7; // Each week, internal auth keys of a peer are renewed.
        public const int PeerMaxPacketBufferSize = 65_535;
        public const int PeerMaxPacketSplitedSendSize = 1024;
        public const int PeerDelayDeleteDeadPeer = 600;
        public const int PeerMinPort = 1;
        public const int PeerMaxPort = 65_535;
        public const int PeerMaxNodeConnectionPerIp = 1000;
        public const int PeerMaxSemaphoreConnectAwaitDelay = 30_000;
        public const int PeerMaxRangeBlockToSyncPerRequest = 5; // Amount of blocks to sync per range.
        public const int PeerMaxRangeTransactionToSyncPerRequest = 5; // Amount of transactions to sync per range.
        public const bool PeerEnableSyncTransactionByRange = true;
        public const bool PeerEnableSovereignPeerVote = false;
        public const int PeerMaxTaskIncomingConnection = 200;

        /// <summary>
        /// The default P2P port.
        /// </summary>
        public const int PeerDefaultPort = 2400;
        public static readonly Dictionary<string, Dictionary<string, int>> BlockchainStaticPeerList = new Dictionary<string, Dictionary<string, int>>()
        {
            { "141.95.229.232", new Dictionary<string, int>() { { "D0BFF4A56F062828939E40E6DFD8A5EF58E28A10CB69E9E281C90802632D0345618CB5DA20736C2BAAC458A1EB5239F012621847B40F76C0CD10EA05CC4FD184", PeerDefaultPort } }}
        }; // This is a static peer list who can't be updated, it's usually used once a peer don't have any peer list saved.


        #endregion

        #region Peer API Settings.

        /// <summary>
        /// Peer API Settings.
        /// </summary>
        public const string PeerDefaultApiIp = "127.0.0.1";
        public const int PeerDefaultApiPort = 2401;
        public const int PeerMaxApiConnectionPerIp = 1000;
        public const int PeerApiMaxConnectionDelay = 30;
        public const int PeerApiMaxEarlierPacketDelay = 60;
        public const int PeerApiMaxPacketDelay = 30;
        public const int PeerApiMaxRangeTransactionToSyncPerRequest = 10; // Amount of transactions to sync per range, by an API.
        public const int PeerApiSemaphoreDelay = 30_000;
        public const int PeerApiMaxRetryRequest = 5;

        #endregion

        #region Sovereign Object Getter class

        private static readonly ClassSovereignUpdateGetter SovereignUpdateGetter = new ClassSovereignUpdateGetter();

        #endregion

        #region Mining Settings.

        /// <summary>
        /// Mining Settings.
        /// </summary>
        public static readonly BigInteger MiningMinDifficulty = 1000;
        public const int MiningMinInstructionsCount = 3;
        public static ClassMiningPoWaCSettingObject DefaultMiningPocSettingObject = new ClassMiningPoWaCSettingObject(true);
        public static ClassMiningPoWaCSettingObject CurrentMiningPoWaCSettingObject(long blockHeight) => SovereignUpdateGetter.GetLastSovereignUpdateMiningPocSettingObject(blockHeight);

        #endregion

    }
}
