using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class IAPManager : MonoBehaviour, IDetailedStoreListener {
    
    IStoreController m_StoreController;
    IAppleExtensions m_AppleExtensions;

    public string noAdsProductId = "remove_ads";
    public string fiveHintsProductId = "five_hints";
    public string fiveSkipsProductId = "five_skips";

    [SerializeField] GameObject noAdsButton = null;
    [SerializeField] GameObject settingsPanel = null;
    [SerializeField] GameObject restoreSuccessModal = null;
    [SerializeField] GameObject restoreFailedModal = null;

    [SerializeField] GameObject hintsLabel = null;
    [SerializeField] GameObject skipsLabel = null;
    [SerializeField] GameObject moreHints = null;
    [SerializeField] GameObject moreSkips = null;

    [SerializeField] GameObject buyBackground = null;
    [SerializeField] GameObject buyHintsModal = null;
    [SerializeField] GameObject buySkipsModal = null;
    [SerializeField] GameObject loadingIcon = null;

    void Start() {
        InitializePurchasing();
        //UpdateWarningMessage();
    }

    void InitializePurchasing() {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(noAdsProductId, ProductType.NonConsumable);
        builder.AddProduct(fiveHintsProductId, ProductType.Consumable);
        builder.AddProduct(fiveSkipsProductId, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
        Debug.Log("In-App Purchasing successfully initialized");

        m_StoreController = controller;
        m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();

        //UpdateUI();
    }

    public void Restore() {
        m_AppleExtensions.RestoreTransactions(OnRestore);
    }

    void OnRestore(bool success, string error) {
        var restoreMessage = "";
        if (success) {
            // This does not mean anything was restored,
            // merely that the restoration process succeeded.
            restoreMessage = "Restore Successful";
            restoreSuccessModal.SetActive(true);
        } else {
            // Restoration failed.
            restoreMessage = $"Restore Failed with error: {error}";
            restoreFailedModal.SetActive(true);
        }
        settingsPanel.SetActive(false);

        Debug.Log(restoreMessage);
    }

    public void ShowMoreHintsModal() {
        if (GameManager.Instance.hintsRemaining == 0) {
            buyBackground.SetActive(true);
            buyHintsModal.SetActive(true);
        }
    }

    public void ShowMoreSkipsModal() {
        if (GameManager.Instance.skipsRemaining == 0) {
            buyBackground.SetActive(true);
            buySkipsModal.SetActive(true);
        }
    }

    public void BuyNoAds() {
        m_StoreController.InitiatePurchase(noAdsProductId);
    }

    public void BuyFiveHints() {
        if (GameManager.Instance.hintsRemaining == 0) {
            loadingIcon.SetActive(true);
            buyHintsModal.SetActive(false);
            m_StoreController.InitiatePurchase(fiveHintsProductId);
        }
    }

    public void BuyFiveSkips() {
        if (GameManager.Instance.skipsRemaining == 0) {
            loadingIcon.SetActive(true);
            buySkipsModal.SetActive(false);
            m_StoreController.InitiatePurchase(fiveSkipsProductId);
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
        var product = args.purchasedProduct;

        //Add the purchased product to the players inventory
        if (product.definition.id == fiveHintsProductId) {
            GameManager.Instance.hintsRemaining += 5;
            loadingIcon.SetActive(false);
            buyHintsModal.SetActive(false);
            buyBackground.SetActive(false);
            UpdateHintsUI();
        } else if (product.definition.id == fiveSkipsProductId) {
            GameManager.Instance.skipsRemaining += 5;
            loadingIcon.SetActive(false);
            buySkipsModal.SetActive(false);
            buyBackground.SetActive(false);
            UpdateSkipsUI();
        } else if (product.definition.id == noAdsProductId) {
            // Print if the ad receipt has been received
            bool result = HasNoAds();
            Debug.Log("Purchase " + result);
            if (HasNoAds()) {
                GameManager.Instance.adsRemoved = true;
                noAdsButton.SetActive(false);
            }
        }

        Debug.Log($"Processing Purchase: {product.definition.id}");
        //UpdateUI();

        return PurchaseProcessingResult.Complete;
    }

    /*void UpdateUI(){
        hasNoAdsText.text = HasNoAds() ? "No ads will be shown" : "Ads will be shown";
    }*/

    void UpdateHintsUI() {
        hintsLabel.GetComponent<Text>().text = "x " + GameManager.Instance.hintsRemaining.ToString();
        moreHints.SetActive(false);
    }

    void UpdateSkipsUI() {
        skipsLabel.GetComponent<Text>().text = "x " + GameManager.Instance.skipsRemaining.ToString();
        moreSkips.SetActive(false);
    }

    bool HasNoAds() {
        var noAdsProduct = m_StoreController.products.WithID(noAdsProductId);
        return noAdsProduct != null && noAdsProduct.hasReceipt;
    }

    public void OnInitializeFailed(InitializationFailureReason error) {
        OnInitializeFailed(error, null);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message) {
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

        if (message != null) {
            errorMessage += $" More details: {message}";
        }

        Debug.Log(errorMessage);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");

        loadingIcon.SetActive(false);
        buyBackground.SetActive(false);
        buyHintsModal.SetActive(false);
        buySkipsModal.SetActive(false);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription) {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
        $" Purchase failure reason: {failureDescription.reason}," +
        $" Purchase failure details: {failureDescription.message}");

        loadingIcon.SetActive(false);
        buyBackground.SetActive(false);
        buyHintsModal.SetActive(false);
        buySkipsModal.SetActive(false);
    }

    /*void UpdateWarningMessage() {
        GetComponent<UserWarningAppleAppStore>().UpdateWarningText();
    }*/
}
