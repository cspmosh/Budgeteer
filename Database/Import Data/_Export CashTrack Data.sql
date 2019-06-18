use cashtrack_production;

# Export tables from CashTrack_Production
SELECT
	A.number, 
	A.name, 
	A.account_type, 
	A.balance, 
	A.active
FROM
	Accounts A
ORDER BY A.name
INTO OUTFILE 'C:\\Develop\\Budgeteer\\Database\\Import Data\\Accounts.txt';

SELECT
	C.name
FROM categories C
ORDER BY C.name
INTO OUTFILE 'C:\\Develop\\Budgeteer\\Database\\Import Data\\Categories.txt';

SELECT 
	C.name,
	B.budget_amount,
	B.start_date
FROM 
	Budgets B, 
	Categories C
WHERE
	B.Categories_id = C.id 
ORDER BY C.Name, B.Start_Date
INTO OUTFILE 'C:\\Develop\\Budgeteer\\Database\\Import Data\\Budgets.txt';

SELECT 
	T.description,
	T.amount,
	T.tax_amount,
	C.name,
	(SELECT 
	Case T.Number
		WHEN Length(T.Number) > 0 THEN NULL
		ELSE T.Number
	END),	
	A.name,
	T.date
FROM 
	Transactions T,
	Accounts A, 
	Categories C 
WHERE
	T.Categories_id = C.id AND
	T.accounts_id = A.id	
ORDER BY T.Date
INTO OUTFILE 'C:\\Develop\\Budgeteer\\Database\\Import Data\\Transactions.txt';