namespace LiwaPOS.Shared.Enums
{
    public enum PermissionType
    {
        // Navigation Permissions
        ExitFullscreen,
        ViewAccountScreen,
        ViewStockNavigation,
        AccessManagement,
        ViewMarketNavigation,
        AccessNavigation,
        PerformEndOfDay,
        AdminPinApproval,
        OpenReports,

        // Account Permissions
        CreateAccount,

        // Adisyon Permissions
        UnlockBill,
        RemoveBillTag,
        MergeBills,
        ViewOldBills,
        AddExtraFeature,
        ViewOthersDocuments,
        ModifyBillExistence,
        ChangeOrderQuantity,
        ChangeSelectedOrderQuantity,
        AllowRefund,
        ViewOpenBills,
        ViewBillsDuringPayment,
        CloseTips,
        CanPerformEndOfDay,

        // Report Permissions
        ChangeReportDate,

        // Department Permissions
        ChangeDepartment
    }
}
