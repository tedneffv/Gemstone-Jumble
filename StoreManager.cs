using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class StoreManager : MonoBehaviour, IStoreAssets
{

	// Use this for initialization
	void Start ()
	{

	}


	public int GetVersion ()
	{
		return 0;
	}

	public VirtualCurrency [] GetCurrencies ()
	{
		return new VirtualCurrency[] {COIN_CURRENCY};
	}

	public VirtualGood[] GetGoods ()
	{
		return new VirtualGood[] {JEWEL_SWAP_GOOD, ROW_DESTRUCTION_GOOD, POWER_BOMB_GOOD, MULTI_STAR_GOOD, SINGLE_STAR_GOOD, FIVE_LIVES_GOOD};
	}

	public VirtualCurrencyPack[] GetCurrencyPacks ()
	{
		return new VirtualCurrencyPack[] {FIVE_THOUSAND_COIN_PACK, THIRTEEN_THOUSAND_SEVENHUNDRED_FIFTY_COIN_PACK, THIRTY_FIVE_THOUSAND_COIN_PACK, EIGHTY_THOUSAND_COIN_PACK,
			ONE_HUNDRED_EIGHTY_THOUSAND_COIN_PACK, FIVE_HUNDRED_THOUSAND_COIN_PACK};
	}

	public VirtualCategory[] GetCategories ()
	{
		return new VirtualCategory[] {GENERAL_CATEGORY};
	}

	/** Static Final Members **/


	/** Virtual Currencies **/

	public static VirtualCurrency COIN_CURRENCY = new VirtualCurrency (
		"Coin", 							// Name
		"Coin currency",					// Description
		"coin_currency_ID"					// Item ID
	);


	public void BuyFiveThousandCoinPack () {
		StoreInventory.BuyItem ("coins_5000_ID");
	}


	/** Virtual Currency Packs **/

	// 0 extra coins 50 coins per cent
	public static VirtualCurrencyPack FIVE_THOUSAND_COIN_PACK = new VirtualCurrencyPack (
		"5000 Coins", 					// Name
		"5000 coin currency units",     // Description
		"coins_5000_ID",					// Item ID
		5000,							// Number of currencies in the pack
		"coin_currency_ID",				// ID of the currency associated with this pack
		new PurchaseWithMarket (// Purchase type (with real money $)
			"5000_coin_pack",    		   // Product ID
			0.99							   // Price (in real money $)
		)
	);

	// 1250 Extra Coins 55 coins per cent
	public static VirtualCurrencyPack THIRTEEN_THOUSAND_SEVENHUNDRED_FIFTY_COIN_PACK = new VirtualCurrencyPack (
		"13750 Coins", 					// Name
		"13750 coin currency units",     // Description«
		"coins_13750_ID",					// Item ID
		13750,							// Number of currencies in the pack
		"coin_currency_ID",				// ID of the currency associated with this pack
		new PurchaseWithMarket (// Purchase type (with real money $)
	        "13750_coin_pack",    		   // Product ID
	        2.49							   // Price (in real money $)
		)
	);

	// 5000 Extra Coins 60 coins per cent
	public static VirtualCurrencyPack THIRTY_FIVE_THOUSAND_COIN_PACK = new VirtualCurrencyPack (
		"30000 Coins", 					// Name
		"30000 coin currency units",     // Description
		"coins_30000_ID",					// Item ID
		30000,							// Number of currencies in the pack
		"coin_currency_ID",				// ID of the currency associated with this pack
		new PurchaseWithMarket (// Purchase type (with real money $)
			"30000_coin_pack",    		   // Product ID
			4.99							   // Price (in real money $)
		)
	);

	// 15000 Extra Coins 65 coins per cent
	public static VirtualCurrencyPack EIGHTY_THOUSAND_COIN_PACK = new VirtualCurrencyPack (
		"65000 Coins",
		"65000 coin currency units",
		"coins_65000_ID",
		65000,
		"coin_currency_ID",
		new PurchaseWithMarket (
			"65000_coin_pack",
			9.99
		)
	);

	// 40000 Extra Coins 70 coins per cent
	public static VirtualCurrencyPack ONE_HUNDRED_EIGHTY_THOUSAND_COIN_PACK = new VirtualCurrencyPack (
		"140000 Coins",
		"140000 coin currency units",
		"coins_140000_ID",
		140000,
		"coin_currency_ID",
		new PurchaseWithMarket (
			"140000_coin_pack",
			19.99
		)
	);

	// 125000 Extra Coins 75 coins per cent
	public static VirtualCurrencyPack FIVE_HUNDRED_THOUSAND_COIN_PACK = new VirtualCurrencyPack (
		"375000 Coins",
		"375000 coin currency units",
		"coins_375000_ID",
		375000,
		"coin_currency_ID",
		new PurchaseWithMarket (
			"375000_coin_pack",
			49.99
		)
	);

	/** Virtual Goods **/

	public static VirtualGood JEWEL_SWAP_GOOD = new SingleUseVG (
		"Jewel Swap",						// Name
		"Swaps Old Jewels for new Jewls",   // Description
		"jewel_swap_ID",					// Item ID
		new PurchaseWithVirtualItem (// Purchase type (with virtual currency)
			"coin_currency_ID",				   // ID of th3e item used to pay with
			1000	                               // Price (amount of coins)
		)
	);

	public static VirtualGood ROW_DESTRUCTION_GOOD = new SingleUseVG (
		"Row Destruction",				// Name
		"Destroys one row or column",   // Description
		"row_destruction_ID",			// Item ID
		new PurchaseWithVirtualItem (// Purchase type (with virtual currency) 
			"coin_currency_ID",
			750
		)
	);

	public static VirtualGood POWER_BOMB_GOOD = new SingleUseVG (
		"Power Bomb",
		"Explodes a square of jewels", 
		"power_bomb_ID",
		new PurchaseWithVirtualItem (
	 		"coin_currency_ID",
	 		500
		)
	);

	public static VirtualGood MULTI_STAR_GOOD = new SingleUseVG (
		"Multi Star",
		"Destroys 5 jewels with 5 stars",
		"multi_star_ID",
		new PurchaseWithVirtualItem (
			"coin_currency_ID",
			250
		)
	);

	public static VirtualGood SINGLE_STAR_GOOD = new SingleUseVG (
		"Single Star",
		"Destroys 1 jewel with 1 star",
		"single_star_ID",
		new PurchaseWithVirtualItem (
			"coin_currency_ID",
			100
		)
	);

	public static VirtualGood FIVE_LIVES_GOOD = new SingleUseVG (
		"Five Lives",
		"Gives player 5 more lives",
		"five_lives_ID",
		new PurchaseWithVirtualItem (
			"coin_currency_ID",
			5000
		)
	);

	/** Virtual Catagories **/

	public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory (
		"General", new List<string> (new string[] {
			JEWEL_SWAP_GOOD.ID,
			ROW_DESTRUCTION_GOOD.ID,
			POWER_BOMB_GOOD.ID,
 			MULTI_STAR_GOOD.ID,
			SINGLE_STAR_GOOD.ID,
			FIVE_LIVES_GOOD.ID
		})
	);

}
