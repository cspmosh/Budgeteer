use budgeteer;

# Insert me
# INSERT INTO users (user_id, password, salt, first_name, last_name, email_address, active) VALUES ('JTurner', '3CFDEB75722B1121E47E81BE9B00F4C47619D835', 'LfVeKv6NVXmT', 'Joshua', 'Turner', 'cs.pmosh@gmail.com', 1);

# Insert my permissions
# INSERT INTO application_settings(setting, value, user_id) VALUES ('Auto-Copy Budgets', 'ON', 'JTurner');

# Insert user security questions
#INSERT INTO user_security_questions(user_id, security_question_id, answer) values ('JTurner', 2, 'Mount Vernon');
#INSERT INTO user_security_questions(user_id, security_question_id, answer) values ('JTurner', 2, 'Answer2');
#INSERT INTO user_security_questions(user_id, security_question_id, answer) values ('JTurner', 3, 'Answer3');

# Insert pre-defined categories into categories table

INSERT INTO categories (description, active, user_id) values ('Automobile', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Charity/Donation', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Clothing', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Entertainment', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Food/Household', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Insurance', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Job Income', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Job Expense', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Loan', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Other Expense', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Other Income', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Pet Expense', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Rent/Mortgage', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Savings Fund', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Subscription', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Travel/Vacation', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Utility', 1, 'JTurner');
INSERT INTO categories (description, active, user_id) values ('Credit Card Payment', 1, 'JTurner');

# Begin importing CashTrack Data

# Import txt files into tables

LOAD DATA INFILE 'C:\\inetpub\\wwwroot\\Staging\\Accounts.txt' 
INTO TABLE Accounts (@Number, @Name, @Type, @Balance, @Active)
SET Number = RIGHT(@Number, 4),
	Name = @Name,
	Type = @Type,
	Balance = @Balance,
	Active = @Active,
	User_id = 'JTurner';

LOAD DATA INFILE 'C:\\inetpub\\wwwroot\\Staging\\Categories.txt'
INTO TABLE Subcategories (@Description) 
SET Category_ID = (SELECT Category_ID FROM categories WHERE user_ID = 'JTurner' ORDER BY 'category_id' LIMIT 1),
	Description = @Description, 
	Type = 'Expense',
	Sinking_Fund = 0,
	Balance = 0.00,
	Active = 1,
	User_id = 'JTurner';

UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Automobile') WHERE Description IN ('Gasoline', 'Vehicle Maintenance', '@Vehicle - Annual Plates', '@Vehicle - Oil Changes') AND User_ID = 'JTurner';
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Charity/Donation') WHERE Description IN ('Compassion', 'Tithes') AND User_ID = 'JTurner';
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Clothing') WHERE Description IN ('Clothing') AND User_ID = 'JTurner';
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Credit Card Payment') WHERE Description IN ('Credit Card Payment') AND User_ID = 'JTurner';
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Entertainment') WHERE Description IN ('Entertainment') AND User_ID = 'JTurner'; 
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Food/Household') WHERE Description IN ('Groceries', 'Household Items') AND User_ID = 'JTurner';
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Insurance') WHERE Description IN ('@Vehicle - Insurance', '@Renter\'s Insurance') AND User_ID = 'JTurner'; 
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Job Income'), type = 'Income' WHERE Description IN ('Primary Solutions', 'CRC', 'VISTA') AND User_ID = 'JTurner'; 
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Job Expense') WHERE Description IN ('Job Expense') AND User_ID = 'JTurner';  
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Loan') WHERE Description IN ('Smart Car Payment', 'Smart Car Payment 2', 'Stafford Loan 1', 'Stafford Loan 2', 'Parent Plus Loan', 'Perkins Loan') AND User_ID = 'JTurner';  
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Other Expense') WHERE Description IN ('Haircut', 'Graduation Expense', 'Budgeteer', 'Birth Control', 'Other Expense', 'Christmas Gifts', 'Optometrist', 'Christmas Budget', 'Credit Card Payment', 'Allowance (Josh)', 'Allowance (Savannah)') AND User_ID = 'JTurner'; 
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Other Income') WHERE Description IN ('Other Income') AND User_ID = 'JTurner'; 
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Pet Expense') WHERE Description IN ('Lucas', 'Pet Expenses') AND User_ID = 'JTurner'; 
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Rent/Mortgage') WHERE Description IN ('Apartment Rent') AND User_ID = 'JTurner';  
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Savings Fund') WHERE Description IN ('@Christmas Savings', '@Doctor Savings', '@Louisiana Savings', 'Monthly Savings', 'Big Spending Savings', 'Emergency Savings', 'Savings') AND User_ID = 'JTurner';  
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Subscription') WHERE Description IN ('Columbus Dispatch', 'Internet Bill', 'Cable', 'Cell Phone') AND User_ID = 'JTurner';
UPDATE Subcategories SET Category_ID = (SELECT category_ID from categories where user_id = 'JTurner' and description = 'Utility') WHERE Description IN ('Electric Bill', 'Water Bill') AND User_ID = 'JTurner';

LOAD DATA INFILE 'C:\\inetpub\\wwwroot\\Staging\\Budgets.txt'
INTO TABLE Budgets (@subcatg, amount, start_date)
SET Subcategory_ID = (SELECT Subcategory_ID FROM Subcategories WHERE Description = @subcatg AND user_id = 'JTurner'),
	User_ID = 'JTurner';

LOAD DATA INFILE 'C:\\inetpub\\wwwroot\\Staging\\Transactions.txt'
INTO TABLE Transactions (Description, Amount, Tax_Amount, @Subcategory, Check_Number, @Account, Date)
SET Subcategory_ID = (SELECT Subcategory_ID FROM Subcategories WHERE Description = @Subcategory and user_id = 'JTurner'),
	Account_ID = (SELECT Account_ID FROM Accounts WHERE Name = @Account and User_id = 'JTurner'),
	User_ID = 'JTurner';
	