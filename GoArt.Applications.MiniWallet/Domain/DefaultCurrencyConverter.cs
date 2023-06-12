using System.Globalization;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using GoArt.Applications.MiniWallet.Extensions;

namespace GoArt.Applications.MiniWallet.Domain;

/// <summary>
/// https://www.codementor.io/@dewetvanthomas/tutorial-currency-converter-application-for-c-121yicb1es
/// </summary>
public class DefaultCurrencyConverter : ICurrencyConverter
{
    private decimal GetCurrencyRateInEuro(Currency currency)
    {
        // Create with currency parameter, a valid RSS url to ECB euro exchange rate feed
        string rssUrl = string.Concat("http://www.ecb.int/rss/fxref-", currency.CurrencyCode.ToLower() + ".html");

        // Create & Load New Xml Document
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        doc.Load(rssUrl);

        // Create XmlNamespaceManager for handling XML namespaces.
        System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("rdf", "http://purl.org/rss/1.0/");
        nsmgr.AddNamespace("cb", "http://www.cbwiki.net/wiki/index.php/Specification_1.1");

        // Get list of daily currency exchange rate between selected "currency" and the EURO
        System.Xml.XmlNodeList nodeList = doc.SelectNodes("//rdf:item", nsmgr)!;

        // Loop Through all XMLNODES with daily exchange rates
        foreach (System.Xml.XmlNode node in nodeList)
        {
            // Create a CultureInfo, this is because EU and USA use different sepperators in float (, or .)
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";

            try
            {
                // Get currency exchange rate with EURO from XMLNODE
                decimal exchangeRate = decimal.Parse(
                    node.SelectSingleNode("//cb:statistics//cb:exchangeRate//cb:value", nsmgr)!.InnerText,
                    NumberStyles.Any,
                    ci);

                return exchangeRate;
            }
            catch { }
        }

        return 0;
    }

    public  MoneyAmount GetExchangeRate(MoneyAmountWithCurrency from, Currency to)
    {
        // Convert Euro to Euro
        if (from.Currency == Currency.Euro && to == Currency.Euro)
        {
            return from.Amount;
        }

        // First Get the exchange rate of both currencies in euro
        decimal toRate = GetCurrencyRateInEuro(to);
        decimal fromRate = GetCurrencyRateInEuro(from.Currency);

        decimal value = 0;
        //int wholePart = 0;
        //int pennyPart = 0;

        // Convert Between Euro to Other Currency
        if (from.Currency == Currency.Euro)
        {
            value = Math.Round(from.Amount.Value * toRate, 2);

        }
        else if (from.Currency == Currency.Euro)
        {
            value = from.Amount.Value / fromRate;
        }
        else
        {
            // Calculate non EURO exchange rates From A to B
            value = (from.Amount.Value * toRate) / fromRate;
        }

        return value.ConvertToMoneyAmount();
    }
}