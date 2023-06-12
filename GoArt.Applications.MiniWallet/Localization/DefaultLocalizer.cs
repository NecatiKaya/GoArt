namespace GoArt.Applications.MiniWallet.Localization;

public sealed class DefaultLocalizer : ILocalizer
{
    public DefaultLocalizer()
    {

    }

    public string Localize(string lang, string key)
    {
        string _key = key + "_" + lang;
        string result = _key switch
        {
            "NOT_A_VALID_MONEY_AMOUNT_TITLE_tr" => "Geçersiz Miktar",
            "NOT_A_VALID_MONEY_AMOUNT_TITLE_en" => "Unsupported Amount",
            "NOT_A_VALID_MONEY_AMOUNT_DETAIL_tr" => "Miktar (tam ve kuruş) sıfırdan büyük olmalıdır. Kuruş 99'dan büyük olamaz.",
            "NOT_A_VALID_MONEY_AMOUNT_DETAIL_en" => "Amount (whole and fraction amount) should be greater than zero. Fraction part can not be greater than 99.",
            "NOT_A_SUPPORTED_CURRENCY_TITLE_tr" => "Geçersiz Döviz",
            "NOT_A_SUPPORTED_CURRENCY_TITLE_en" => "Unsupported Currency",
            "NOT_A_SUPPORTED_CURRENCY_DETAIL_tr" => "Para biri harf duyarlı olacak şekilde 'TRY', 'USD', 'POUND' ve 'EURO' olabilir.",
            "NOT_A_SUPPORTED_CURRENCY_DETAIL_en" => "Please provide currency one of those (case sensitive) 'TRY', 'USD', 'POUND' and 'EURO'.",
            "NOT_ENOUGH_AMOUNT_IN_WALLET_FOR_CURRENCY_TITLE_tr" => "Cüzdanda Yetersiz Döviz",
            "NOT_ENOUGH_AMOUNT_IN_WALLET_FOR_CURRENCY_TITLE_en" => "Not Enough Amount In Wallet With Currency",
            "NOT_ENOUGH_AMOUNT_IN_WALLET_FOR_CURRENCY_DETAIL_tr" => "Cüzdanınızda ilgili döviz ile yeterli miktar mevcut değil.",
            "NOT_ENOUGH_AMOUNT_IN_WALLET_FOR_CURRENCY_DETAIL_en" => "You don't have enough amount by given currency",
            "NO_AMOUNT_IN_WALLET_TITLE_tr" => "Hiç Para Yok",
            "NO_AMOUNT_IN_WALLET_TITLE_en" => "No Amount In Wallet",
            "NO_AMOUNT_IN_WALLET_DETAIL_tr" => "Cüzdanınızda hiç para mevcut değil.",
            "NO_AMOUNT_IN_WALLET_DETAIL_en" => "You dont have any money in wallet.",
            "NOT_ENOUGN_AMOUNT_IN_WALLET_TITLE_tr" => "Cüzdanınızda Yeterli Miktar Mevcut Değil",
            "NOT_ENOUGN_AMOUNT_IN_WALLET_TITLE_en" => "Not Enough Amount",
            "NOT_ENOUGN_AMOUNT_IN_WALLET_DETAIL_tr" => "Cüzdanınızda mevcut para biriminde veya totalde yeterli miktarda para mevcut değil.",
            "NOT_ENOUGN_AMOUNT_IN_WALLET_DETAIL_en" => "You dont have enough amount in your wallet or a given currency or all currencies.",
            "WALLET_NAME_NOT_SPECIFIED_TITLE_tr" => "Geçersiz Cüzdan Adı",
            "WALLET_NAME_NOT_SPECIFIED_TITLE_en" => "Wallet Name Not Specified",
            "WALLET_NAME_NOT_SPECIFIED_DETAIL_tr" => "Cüzdan adı girilmedi veya geçersiz olarak girildi.",
            "WALLET_NAME_NOT_SPECIFIED_DETAIL_en" => "Please provide a wallet name.",
            "NOT_SUPPORTED_WALLET_ID_TITLE_tr" => "Geçersiz Cüzdan Kimliği",
            "NOT_SUPPORTED_WALLET_ID_TITLE_en" => "Unsupported Wallet Id",
            "NOT_SUPPORTED_WALLET_ID_DETAIL_tr" => "Cüzdanınız kimliği geçersiz.",
            "NOT_SUPPORTED_WALLET_ID_DETAIL_en" => "Your wallet id is not supported",
            "WALLET_NOT_FOUND_TITLE_tr" => "Cüzdan Bulunamadı",
            "WALLET_NOT_FOUND_TITLE_en" => "Wallet Not Found",
            "WALLET_NOT_FOUND_DETAIL_tr" => "Belirtilen Id ile cüzdan bulunamadı.",
            "WALLET_NOT_FOUND_DETAIL_en" => "Wallet could not be found by the given id.",
            "DEPOSIT_NOT_ALLOWED_TITLE_tr" => "Para Yatıralamaz",
            "DEPOSIT_NOT_ALLOWED_TITLE_en" => "Deposit Not Allowed",
            "DEPOSIT_NOT_ALLOWED_DETAIL_tr" => "Herhangi bir nedenden dolayı para yatıramazsınız.",
            "DEPOSIT_NOT_ALLOWED_DETAIL_en" => "For any reason, you can not deposit",
            "GENERIC_EXCEPTION_TITLE_tr" => "Bilinmeyen Hata Oluştu",
            "GENERIC_EXCEPTION_TITLE_en" => "Unkown Exception Occured",
            "GENERIC_EXCEPTION_DETAIL_tr" => "Çok özür dileriz. Tahmin edilemeyen bir hata ile karşılaştık",
            "GENERIC_EXCEPTION_DETAIL_en" => "We are sorry to have an unforseen error.",
            _ => ""
        };

        return result;
    }
}