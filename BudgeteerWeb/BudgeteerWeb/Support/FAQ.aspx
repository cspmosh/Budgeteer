<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FAQ.aspx.vb" Inherits="BudgeteerWeb.FAQ" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/support/faq.aspx">Support</a> > FAQ</asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">
    Frequently Asked Questions     
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h3>Bank Accounts</h3>
    
    <strong>Q: How does Budgeteer use bank accounts?</strong><br />
    A: Bank Accounts are useful for keeping an up-to-date balance based on your spendings and earnings when you specify a bank account in your transactions. Bank accounts are not required, but it's a nice feature to have, especially if you manually balance your checkbooks or just want to know how much money you have available.<br /><br />
     
    <h3>Budgets</h3>
    
    <strong>Q: What is a budget?</strong><br />
    A: A budget is simply an alotted amount of money set aside for a specific category. In Budgeteer, you have two types of budgets: income budgets and expense budgets. Income budgets are set up to reflect the expected income for a given month and category, whereas expense budgets are set up to reflect the expected or desired expense amount for a given month and category.<br /><br />
    
    <strong>Q: What does "Utilized Dollars" mean?</strong><br />
    A: Utilized dollars are just the amount of money earned or spent compared to the amount budgeted for a given category or subcategory.<br /><br />
    
    <strong>Q: Why is my monthly budgeted dollar amount different from the amount shown available in the utilized dollars?</strong><br />
    A: Subcategories that are defined as a "sinking fund" are meant to carry a balance from month to month. The dollar amount available during a given month for a given subcategory is dependent not only on the current month's budget but also the previous months leftover budgets.<br /><br />
    
    <strong>Q: What does the "Copy Prior Budgets" button do?</strong><br />
    A: Press this button to copy all the budgets from the previous month to the current month. This is very useful for budgets that repeat from month to month so you don't have to manually create them every time. If you already have a budget for a specified subcategory for the current month, the previous month's subcategory will not be copied forward.<br /><br />
    
    <h3>Transactions</h3>
    
    <strong>Q: What is a transaction?</strong><br />
    A: Transactions are defined as any expenses or deposits that you want to track in Budgeteer. In theory if you're budgeting correctly, every single dollar you earn or spend should be tied to a corresponding category that you've set up a budget for. To see a total amount earned or spent for a given category or subcategory in a given month, look at the "Utilized dollars" under budgets. Transactions may also optionally be tied to a bank account which will automatically adjust the bank account balance.<br /><br />
    
    <strong>Q: What is a category/subcategory?</strong><br/>
    A: Categories and subcategories in Budgeteer are more or less earning and spending categories which you define and use to categorize your transactions and budgets. A category that is assigned to a transaction may or may not have an associated budget.<br /><br />
    
    <strong>Q: What is a sinking fund?</strong><br />
    A: A sinking fund is basically just a subcategory that carries a balance from month to month. The intent of a sinking fund is that if you have a planned expense once or twice a year (such as auto insurance), you can create a budget for all the months leading up to that planned expense and the subcategory balance will carry over from month to month so that at the end you will have the fully funded amount needed. Because of the nature of sinking funds, only expense-based subcategories can have this option turned on.<br /><br />
    
    <h3>General</h3>
    <strong>Q: Is Budgeteer secure?</strong><br />
    A: Besides hashing your account password in the database, Budgeteer doesn't actually store any bank or credit card information aside from the optional 4-digit account number you set for each bank account. So rest with ease, my friend, Budgeteer is safe and secure.<br /><br />
    
    <br /><br />
    <strong>Didn't see an answer to your question? Send an email to <a href="mailto:budgeteer.app@gmail.com?subject='Support Question'">budgeteer.app@gmail.com</a> with your question!    </strong>
</asp:Content>