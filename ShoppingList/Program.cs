﻿Dictionary<string, decimal> itemsForPurchase = new Dictionary<string, decimal>()
{
    {"Bread", 1.00M },
    {"Milk", 1.89M },
    {"Chips", 2.50M },
    {"Chicken", 8.99M },
    {"Butter", 3.23M },
    {"Steak", 12.40M },
    {"Bananas", 0.99M },
    {"Apples", 5.00M }
};

List<string> shoppingList = new List<string>();
bool continueResult = true;

while (continueResult)
{
    Console.WriteLine("Here are the items available for purchase:");

    for (int i = 0; i < itemsForPurchase.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {itemsForPurchase.ElementAt(i).Key}: {itemsForPurchase.ElementAt(i).Value:C}");
    }

    Console.WriteLine("Please enter the number of the item you would like to purchase:");
    int itemNumber = Convert.ToInt32(Console.ReadLine());

    if (itemNumber < 1 || itemNumber > itemsForPurchase.Count)
    {
        do
        {
            Console.WriteLine($"That is not a valid number. Please enter a number between 1 and {itemsForPurchase.Count}.");
            itemNumber = Convert.ToInt32(Console.ReadLine());
        }
        while (itemNumber < 1 || itemNumber > itemsForPurchase.Count);
    }

    Console.WriteLine($"{itemsForPurchase.ElementAt(itemNumber - 1).Key}: {itemsForPurchase.ElementAt(itemNumber - 1).Value:C} has been added to your shopping list.");

    shoppingList.Add(itemsForPurchase.ElementAt(itemNumber - 1).Key);

    Console.WriteLine("Would you like to add another item? (y/n)");
    continueResult = ToContinue();
}
Console.WriteLine("Thanks for your order!");
Console.WriteLine("Here is your receipt: ");
Console.WriteLine("======================");
foreach (string item in shoppingList)
{
    if (itemsForPurchase.ContainsKey(item))
    {
        decimal itemPrice = itemsForPurchase.GetValueOrDefault(item);
        Console.WriteLine($"{item,10} | ${itemPrice}");
    }
}
Console.WriteLine($"The total price of your items is: {TotalPrice():C}");
Console.WriteLine($"Your most expensive item is: {GetMostExpensiveItem():C}");
Console.WriteLine($"Your least expensive item is: {GetLeastExpensiveItem():C}");
Console.WriteLine("\nItems in order from least expensive to most expensive:");
GetOrderedList(itemsForPurchase);

List<decimal> GetItemValueFromDictionary(Dictionary<string, decimal> inventoryList, List<string> purchaseList)
{
    List<decimal> purchaseListPriceList = new List<decimal>();
    foreach (string item in purchaseList)
    {
        if (inventoryList.ContainsKey(item))
        {
            decimal itemPrice = inventoryList.GetValueOrDefault(item);
            purchaseListPriceList.Add(itemPrice);
        }
    }
    return purchaseListPriceList;
}

void GetOrderedList(Dictionary<string, decimal> inventoryList)
{
    Dictionary<string, decimal> shoppingListItemsAndPrice = new Dictionary<string, decimal>();
    foreach (string item in shoppingList)
    {
        if (inventoryList.ContainsKey(item))
        {
            decimal itemPrice = itemsForPurchase.GetValueOrDefault(item);

            if (!shoppingListItemsAndPrice.ContainsKey(item))
            {
                shoppingListItemsAndPrice.Add(item, itemPrice);
            }
        }
    }
    foreach(KeyValuePair<string, decimal> item in shoppingListItemsAndPrice.OrderBy(key => key.Value))
    {
        Console.WriteLine($"{item.Key,10} | ${item.Value}");
    }
}

decimal GetMostExpensiveItem()
{
    decimal highestPrice = GetItemValueFromDictionary(itemsForPurchase, shoppingList).OrderByDescending(x => x).FirstOrDefault();
    return highestPrice;
}

decimal GetLeastExpensiveItem()
{
    decimal lowestPrice = GetItemValueFromDictionary(itemsForPurchase, shoppingList).OrderBy(x => x).FirstOrDefault();
    return lowestPrice;
}

bool ToContinue()
{
    string answer = Console.ReadLine();
    if (answer.Contains("y") || answer.Contains("yes"))
    {
        return true;
    }
    return false;
}

decimal TotalPrice()
{
    decimal totalPrice = 0;
    foreach (string item in shoppingList)
    {
        if (itemsForPurchase.ContainsKey(item))
        {
            totalPrice = Decimal.Add(totalPrice, itemsForPurchase.GetValueOrDefault(item));
        }
    }
    return totalPrice;
}