﻿namespace SeguraChain_Lib.Instance.Node.Network.Services.API.Packet.Explorer
{
    public class ClassApiBlockchainExplorerHtmlContent
    {
		public const string ContentCoinName = "$coinName";
		public const string ContentApiHost = "$apiHost";
		public const string ContentApiPort = "$apiPort";

        public const string Content = @"<html lang='en'>
<title>$coinName - Blockchain Explorer</title>
<meta name='description' content='$coinName blockchain explorer'/>
<meta charset='UTF-8'/>
<meta name='viewport' content='width=device-width,initial-scale=1''>
<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css' integrity='sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm' crossorigin='anonymous'>
<script src='https://code.jquery.com/jquery-3.2.1.slim.min.js' integrity='sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN' crossorigin='anonymous'></script>
<script src='https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js' integrity='sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q' crossorigin='anonymous'></script>
<script src='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js' integrity='sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl' crossorigin='anonymous'></script>
	
<body>

	<div class='container'>
		<h1>Block explorer</h1>
		<div class='table-responsive'>
			<table class='table table-bordered' id='blockchainTable'>
				<thead>
					<tr>
						<th>Height</th>
						<th>Difficulty</th>
						<th>Reward</th>
						<th>Hash</th>
						<th>Created</th>
						<th>Found</th>
						<th>Owner</th>
						<th>Size</th>
						<th>Coin confirmed</th>
						<th>Coin pending</th>
						<th>Total Fee</th>
						<th>Total Transactions</th>
						<th>Total Confirmed</th>
					</tr>
				</thead>
				<tbody id='blockList'/>
			</table>
			<center><button type='button' class='btn btn-default' id='blockMore'>Show More</button></center>
		</div>
	</div>
	
	<div class='container'>
		<h1>Transaction explorer</h1>
		<div class='table-responsive'>
			<table class='table table-bordered' id='transactionTable'>
				<thead>
					<tr>
						<th>Type</th>
						<th>Block Height</th>
						<th>Height Target</th>
						<th>Transaction Hash</th>
						<th>Amount</th>
						<th>Fee</th>
						<th>Payment ID</th>
						<th>Version</th>
						<th>Date send</th>
						<th>Sender</th>
						<th>Receiver</th>
						<th>Status</th>
					</tr>
				</thead>
				<tbody id='transactionList'/>
			</table>
			<center><button type='button' class='btn btn-default' id='transactionMore'>Show More</button></center>
		</div>
	</div>
</body>
	
<style>
.body
{
	background-color: gray;
}
</style>

<script>
	
	var lastBlockHeight = 0;
	var lastBlockHeightUnlocked = 0;
	var apiHost = 'http://$apiHost:$apiPort/';
	var apiMaxBlockPerCall = 10;
	
	function LoadScripts()
	{
		loadScripts();
	}
	
	function GetNetworkStats()
	{
		var xmlHttp = new XMLHttpRequest();
		xmlHttp.onreadystatechange = function()
		{ 
			if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
			{
				var data = JSON.parse(xmlHttp.responseText);
				var apiPacketData = JSON.parse(data.PacketObjectSerialized);
				
				var lastBlockHeight = apiPacketData.BlockchainNetworkStatsObject.LastBlockHeight;
				lastBlockHeightUnlocked = apiPacketData.BlockchainNetworkStatsObject.LastBlockHeightUnlocked;
				
				for(var i = 0; i < apiMaxBlockPerCall; i++)
					GetBlockInformation(lastBlockHeight);
			}
		}
		
		xmlHttp.open('GET', apiHost + 'get_network_stats', true);
		xmlHttp.send();
	}
	
	function GetBlockInformation(blockHeight)
	{
		var xmlHttp = new XMLHttpRequest();
		xmlHttp.contentType = 'text/json';
		xmlHttp.onreadystatechange = function()
		{ 
			if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
			{
				var data = JSON.parse(xmlHttp.responseText);				
				var apiPacketData = JSON.parse(data.PacketObjectSerialized);
			}
		}
	
		xmlHttp.open('POST', apiHost, true);
		xmlHttp.send(doPostPacketSerialized(
			0, 
			{
				'BlockHeight': blockHeight,
				'PacketTimestamp': Math.round((Date.now() / 1000))
			})
		);
		
	}
	
	function doPostPacketSerialized(type, content)
	{
		return {
			'PacketType': type,
			'PacketContentObjectSerialized': content
		};
	}
	
	function loadScripts(urls, length, success)
	{
    	if(length > 0)
		{
        	script = document.createElement('SCRIPT');
        	script.src = urls[length-1];
        	script.onload = function()
			{
				loadScripts(urls, length-1, success);               
			};
				document.getElementsByTagName('head')[0].appendChild(script);
    	}
    	else
        	if(success)
           		success();
	}

	
	urls = [];

	loadScripts(urls, urls.length, function()
	{
		GetNetworkStats();
	});

</script>
</html>";

    }
}
