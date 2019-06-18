delimiter //

##### BANK ACCOUNT PROCEDURES #####
DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_accounts//
CREATE PROCEDURE budgeteer.bsp_get_accounts(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM accounts WHERE accounts.user_id = p_userID ORDER BY accounts.active DESC, accounts.name ASC;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_active_accounts//
CREATE PROCEDURE budgeteer.bsp_get_active_accounts(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM accounts WHERE accounts.user_id = p_userID AND accounts.active = 1 ORDER BY accounts.name;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_account//
CREATE PROCEDURE budgeteer.bsp_get_account(IN p_accountID BIGINT )
BEGIN
	SELECT * FROM accounts WHERE accounts.account_id = p_accountID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_add_account//
CREATE PROCEDURE budgeteer.bsp_add_account(IN p_number VARCHAR(4), IN p_name VARCHAR(50), IN p_type VARCHAR(20), IN p_balance DECIMAL(15,2), IN p_userID VARCHAR(30))
BEGIN
	INSERT INTO accounts (number, name, type, balance, active, user_id) VALUES (p_number, p_name, p_type, p_balance, 1, p_userID);
	SELECT last_insert_id();
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_update_account//
CREATE PROCEDURE budgeteer.bsp_update_account(IN p_accountID BIGINT, IN p_number VARCHAR(4), IN p_name VARCHAR(50), IN p_type VARCHAR(20), IN p_balance DECIMAL(15,2), IN p_active TINYINT(1))
BEGIN
	UPDATE accounts SET name = p_name, number = p_number, type = p_type, balance = p_balance, active = p_active WHERE accounts.account_ID = p_accountID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_delete_account//
CREATE PROCEDURE budgeteer.bsp_delete_account(IN p_accountID BIGINT)
BEGIN
	DELETE FROM accounts WHERE accounts.account_ID = p_accountID;
END//

##### USER PROCEDURES #####
DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_user//
CREATE PROCEDURE budgeteer.bsp_get_user(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM users WHERE users.user_id = p_userID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_user_by_email//
CREATE PROCEDURE budgeteer.bsp_get_user_by_email(IN p_emailAddress VARCHAR(50))
BEGIN
	SELECT * FROM users WHERE users.email_address = p_emailAddress;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_add_user//
CREATE PROCEDURE budgeteer.bsp_add_user(IN p_userID VARCHAR(30), IN p_password VARCHAR(40), IN p_salt VARCHAR(12), IN p_firstName VARCHAR(50), IN p_lastName VARCHAR(50), IN p_emailAddress VARCHAR(50))
BEGIN	
	INSERT INTO users (user_id, password, salt, first_name, last_name, email_address) VALUES (p_userID, p_password, p_salt, p_firstName, p_lastName, p_emailAddress);	
	# prepopulate categories and subcategories
	INSERT INTO categories (description, active, user_id) values ('Automobile', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Charity/Donation', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Clothing', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Entertainment', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Food/Household', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Insurance', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Job Income', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Loans', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Other Expense', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Other Income', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Rent/Mortgage', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Services', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Travel/Vacation', 1, p_userID);
	INSERT INTO categories (description, active, user_id) values ('Utility', 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Automobile' And c.user_id = p_userID), 'Gas', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Clothing' And c.user_id = p_userID), 'Clothing', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Entertainment' And c.user_id = p_userID), 'Entertainment', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Food/Household' And c.user_id = p_userID), 'Groceries', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Food/Household' And c.user_id = p_userID), 'Dining Out', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Food/Household' And c.user_id = p_userID), 'Household Items', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Insurance' And c.user_id = p_userID), 'Auto Insurance', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Insurance' And c.user_id = p_userID), 'Renter\'s Insurance', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Insurance' And c.user_id = p_userID), 'Home Insurance', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Loans' And c.user_id = p_userID), 'Student Loans', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Loans' And c.user_id = p_userID), 'Personal Loans', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Loans' And c.user_id = p_userID), 'Auto Loans', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Other Expense' And c.user_id = p_userID), 'Other', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Services' And c.user_id = p_userID), 'Cable', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Services' And c.user_id = p_userID), 'Internet', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Services' And c.user_id = p_userID), 'Cell Phone', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Utility' And c.user_id = p_userID), 'Electric/Gas', 'Expense', 0, 0.00, 1, p_userID);
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, active, user_id) values ((Select c.category_id from categories c where c.description = 'Utility' And c.user_id = p_userID), 'Water/Sewage', 'Expense', 0, 0.00, 1, p_userID);

END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_security_questions//
CREATE PROCEDURE budgeteer.bsp_get_security_questions()
BEGIN
	SELECT * FROM security_questions;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_security_question_by_id//
CREATE PROCEDURE budgeteer.bsp_get_security_question_by_id(IN p_securityQuestionID BIGINT)
BEGIN
	SELECT * FROM security_questions WHERE security_questions.security_question_id = p_securityQuestionID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_user_security_question_answer//
CREATE PROCEDURE budgeteer.bsp_get_user_security_question_answer(IN p_userID VARCHAR(30), IN p_securityQuestionID BIGINT)
BEGIN
	SELECT * FROM user_security_questions WHERE user_security_questions.user_id = p_userID AND user_security_questions.security_question_id = p_securityQuestionID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_user_security_questions//
CREATE PROCEDURE budgeteer.bsp_get_user_security_questions(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM user_security_questions WHERE user_security_questions.user_id = p_userID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_add_user_security_question//
CREATE PROCEDURE budgeteer.bsp_add_user_security_question(IN p_userID VARCHAR(30), IN p_questionID BIGINT, IN p_answer VARCHAR(50))
BEGIN
	INSERT INTO user_security_questions (user_id, security_question_id, answer) VALUES (p_userID, p_questionID, p_answer);
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_registration//
CREATE PROCEDURE budgeteer.bsp_get_registration(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM user_registrations WHERE user_registrations.user_id = p_userID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_add_registration//
CREATE PROCEDURE budgeteer.bsp_add_registration(IN p_registrationKey CHARACTER(36), IN p_userID VARCHAR(30), IN p_emailAddress VARCHAR(50))
BEGIN
	INSERT INTO user_registrations (registration_key, user_id, email_address) VALUES (p_registrationKey, p_userID, p_emailAddress);
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_delete_registration//
CREATE PROCEDURE budgeteer.bsp_delete_registration(IN p_userID VARCHAR(30))
BEGIN
	DELETE FROM user_registrations WHERE user_registrations.user_id = p_userID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_activate_user//
CREATE PROCEDURE budgeteer.bsp_activate_user(IN p_userID VARCHAR(30))
BEGIN
	UPDATE users SET users.active = 1 WHERE users.user_id = p_userID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_update_user_password//
CREATE PROCEDURE budgeteer.bsp_update_user_password(IN p_userID VARCHAR(30), IN p_newPassword VARCHAR(40))
BEGIN
	UPDATE users SET password = p_newPassword WHERE users.user_id = p_userID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_update_user_email//
CREATE PROCEDURE budgeteer.bsp_update_user_email(IN p_userID VARCHAR(30), IN p_newEmailAddress VARCHAR(50))
BEGIN
	UPDATE users SET email_address = p_newEmailAddress WHERE users.user_id = p_userID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_delete_user//
CREATE PROCEDURE budgeteer.bsp_delete_user(IN p_userID VARCHAR(30))
BEGIN
	DELETE FROM users WHERE users.user_id = p_userID;
END//

##### CATEGORY PROCEDURES #####
DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_categories//
CREATE PROCEDURE budgeteer.bsp_get_categories(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM categories WHERE categories.user_id = p_userID ORDER BY categories.active DESC, categories.description ASC;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_active_categories//
CREATE PROCEDURE budgeteer.bsp_get_active_categories(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM categories WHERE categories.user_id = p_userID AND categories.active = 1 ORDER BY categories.description ASC;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_category//
CREATE PROCEDURE budgeteer.bsp_get_category(IN p_categoryID BIGINT )
BEGIN
	SELECT * FROM categories WHERE categories.category_id = p_categoryID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_add_category//
CREATE PROCEDURE budgeteer.bsp_add_category(IN p_description VARCHAR(50), IN p_active TINYINT(1), IN p_userID VARCHAR(30)) 
BEGIN
	INSERT INTO categories (description, active, user_id) VALUES (p_description, p_active, p_userID);
	SELECT last_insert_id();
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_update_category//
CREATE PROCEDURE budgeteer.bsp_update_category(IN p_categoryID BIGINT, IN p_description VARCHAR(50), IN p_active TINYINT(1))
BEGIN
	UPDATE categories SET categories.description = p_description, categories.active = p_active WHERE categories.category_id = p_categoryID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_delete_category//
CREATE PROCEDURE budgeteer.bsp_delete_category(IN p_categoryID BIGINT)
BEGIN
	DELETE FROM categories WHERE categories.category_ID = p_categoryID;
END//

##### SUBCATEGORY PROCEDURES #####
DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_subcategories//
CREATE PROCEDURE budgeteer.bsp_get_subcategories(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM subcategories WHERE subcategories.user_id = p_userID ORDER BY subcategories.active DESC, subcategories.description ASC;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_active_subcategories//
CREATE PROCEDURE budgeteer.bsp_get_active_subcategories(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM subcategories WHERE subcategories.user_id = p_userID AND active = 1 ORDER BY subcategories.description ASC;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_subcategory//
CREATE PROCEDURE budgeteer.bsp_get_subcategory(IN p_subcategoryID BIGINT )
BEGIN
	SELECT * FROM subcategories WHERE subcategories.subcategory_id = p_subcategoryID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_subcategory_count_by_categoryID//
CREATE PROCEDURE budgeteer.bsp_get_subcategory_count_by_categoryID(IN p_categoryID BIGINT)
BEGIN
	SELECT count(*) FROM subcategories WHERE subcategories.category_id = p_categoryID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_subcategories_by_categoryID//
CREATE PROCEDURE budgeteer.bsp_get_subcategories_by_categoryID(IN p_categoryID BIGINT, IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM subcategories WHERE subcategories.category_id = p_categoryID AND subcategories.user_id = p_userID ORDER BY subcategories.active DESC, subcategories.description ASC;
END// 

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_active_subcategories_by_categoryID//
CREATE PROCEDURE budgeteer.bsp_get_active_subcategories_by_categoryID(IN p_categoryID BIGINT, IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM subcategories WHERE subcategories.category_id = p_categoryID AND subcategories.user_id = p_userID AND subcategories.active = 1 ORDER BY subcategories.description ASC;
END// 

DROP PROCEDURE IF EXISTS Budgeteer.bsp_add_subcategory//
CREATE PROCEDURE budgeteer.bsp_add_subcategory(IN p_categoryID BIGINT, IN p_description VARCHAR(50), IN p_type VARCHAR(10), IN p_sinkingFund TINYINT(1), IN p_balance DECIMAL(15,2), IN p_notes VARCHAR(255), IN p_active TINYINT(1), IN p_userID VARCHAR(30)) 
BEGIN
	INSERT INTO subcategories (category_id, description, type, sinking_fund, balance, notes, active, user_id) VALUES (p_categoryID, p_description, p_type, p_sinkingFund, p_balance, p_notes, p_active, p_userID);
	SELECT last_insert_id();
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_update_subcategory//
CREATE PROCEDURE budgeteer.bsp_update_subcategory(IN p_subcategoryID BIGINT, IN p_categoryID BIGINT, IN p_description VARCHAR(50), IN p_type VARCHAR(10), IN p_sinkingFund TINYINT(1), IN p_balance DECIMAL(15,2), IN p_notes VARCHAR(255), IN p_active TINYINT(1))
BEGIN
	UPDATE subcategories SET subcategories.description = p_description, subcategories.type = p_type, subcategories.sinking_fund = p_sinkingFund, subcategories.balance = p_balance, subcategories.category_ID = p_categoryID, subcategories.notes = p_notes, subcategories.active = p_active WHERE subcategories.subcategory_id = p_subcategoryID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_delete_subcategory//
CREATE PROCEDURE budgeteer.bsp_delete_subcategory(IN p_subcategoryID BIGINT)
BEGIN
	DELETE FROM subcategories WHERE subcategories.subcategory_ID = p_subcategoryID;
END//

##### TRANSACTION PROCEDURES #####
DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_transactions//
CREATE PROCEDURE budgeteer.bsp_get_transactions(IN p_userID VARCHAR(30), IN p_year INTEGER, IN p_month INTEGER, IN p_day INTEGER, IN p_checkNum INTEGER, IN p_description VARCHAR(255), IN p_amount DECIMAL(15,2), IN p_taxAmount DECIMAL(15,2), IN p_subcategoryID BIGINT, IN p_accountID BIGINT)
BEGIN
	DECLARE _statement VARCHAR(500);
	SET _statement = CONCAT("SELECT * FROM transactions WHERE transactions.user_id = '", p_userID, "'");
	
	IF (p_year IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, ' AND YEAR(transactions.date) = ', p_year);
	END IF;
	
	IF (p_month IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, ' AND MONTH(transactions.date) = ', p_month);
	END IF;
	
	IF (p_day IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, ' AND DAY(transactions.date) = ', p_day);
	END IF;	
	
	IF (p_checkNum IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, ' AND transactions.check_number = ', p_checkNum);	
	END IF;	
	
	IF (p_description IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, " AND transactions.description like '%", p_description, "%'");	
	END IF;	
		
	IF (p_amount IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, ' AND ABS(transactions.amount) = ABS(', p_amount, ')');	
	END IF;	
		
	IF (p_taxAmount IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, ' AND transactions.tax_amount = ', p_taxAmount);	
	END IF;	
	
	IF (p_subcategoryID IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, ' AND transactions.subcategory_id = ', p_subcategoryID);	
	END IF;	
	
	IF (p_accountID IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, ' AND transactions.account_id = ', p_accountID);	
	END IF;
	
	SET _statement = CONCAT(_statement, ' ORDER BY transactions.date DESC, transactions.transaction_id DESC');
	
	SET @statement = _statement;
	PREPARE dynquery FROM @statement;
	EXECUTE dynquery;
	DEALLOCATE PREPARE dynquery;

END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_transaction//
CREATE PROCEDURE budgeteer.bsp_get_transaction(IN p_transactionID BIGINT)
BEGIN
	SELECT * FROM transactions WHERE transactions.transaction_ID = p_transactionID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_transaction_count_by_subcategoryID//
CREATE PROCEDURE budgeteer.bsp_get_transaction_count_by_subcategoryID(IN p_subcategoryID BIGINT)
BEGIN
	SELECT count(*) FROM transactions WHERE transactions.subcategory_ID = p_subcategoryID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_transaction_count_by_accountID//
CREATE PROCEDURE budgeteer.bsp_get_transaction_count_by_accountID(IN p_accountID BIGINT)
BEGIN
	SELECT count(*) FROM transactions WHERE transactions.account_ID = p_accountID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_add_transaction//
CREATE PROCEDURE budgeteer.bsp_add_transaction(IN p_date DATE, IN p_checkNum INTEGER, IN p_description VARCHAR(255), IN p_amount DECIMAL(15,2), IN p_taxAmount DECIMAL(15,2), IN p_subcategoryID BIGINT, IN p_accountID BIGINT, IN p_userID VARCHAR(30))
BEGIN
	INSERT INTO transactions (date, check_number, description, amount, tax_amount, subcategory_id, account_ID, user_id) VALUES (p_date, p_checkNum, p_description, p_amount, p_taxAmount, p_subcategoryID, p_accountID, p_userID);
	SELECT last_insert_id();
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_update_transaction//
CREATE PROCEDURE budgeteer.bsp_update_transaction(IN p_transactionID BIGINT, IN p_date DATE, IN p_checkNum INTEGER, IN p_description VARCHAR(255), IN p_amount DECIMAL(15,2), IN p_taxAmount DECIMAL(15,2), IN p_subcategoryID BIGINT, IN p_accountID BIGINT)
BEGIN
	UPDATE transactions SET transactions.date = p_date, transactions.check_number = p_checkNum, transactions.description = p_description, transactions.amount = p_amount, transactions.tax_amount = p_taxAmount, transactions.subcategory_id = p_subcategoryID, transactions.account_id = p_accountID WHERE transactions.transaction_id = p_transactionID;	
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_delete_transaction//
CREATE PROCEDURE budgeteer.bsp_delete_transaction(IN p_transactionID BIGINT)
BEGIN
	DELETE FROM transactions WHERE transactions.transaction_id = p_transactionID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_first_transaction_year//
CREATE PROCEDURE budgeteer.bsp_get_first_transaction_year(IN p_userID VARCHAR(30))
BEGIN
	SELECT YEAR(MIN(transactions.date)) FROM transactions WHERE transactions.user_id = p_userID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_first_budget_year//
CREATE PROCEDURE budgeteer.bsp_get_first_budget_year(IN p_userID VARCHAR(30))
BEGIN
	SELECT YEAR(MIN(budgets.start_date)) FROM budgets WHERE budgets.user_id = p_userID;
END//

##### BUDGET PROCEDURES #####
DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_budgets//
CREATE PROCEDURE budgeteer.bsp_get_budgets(IN p_userID VARCHAR(30), IN p_date DATE, IN p_subcategoryType VARCHAR(10))
BEGIN
DECLARE _statement VARCHAR(500);
	SET _statement = CONCAT("SELECT budgets.* FROM budgets, categories, subcategories WHERE budgets.user_id = '", p_userID, "'");
		
	IF (p_date IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, " AND YEAR(budgets.start_date) = YEAR('", p_date, "') AND MONTH(budgets.start_date) = MONTH('", p_date, "')");
	END IF;	
	
	IF (p_subcategoryType IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, " AND subcategories.type = '", p_subcategoryType, "'");
	END IF;
	
	SET _statement = CONCAT(_statement, ' AND budgets.subcategory_id = subcategories.subcategory_id AND subcategories.category_id = categories.category_id ORDER BY budgets.start_date DESC, subcategories.type ASC, categories.description ASC, subcategories.description ASC');
	
	SET @statement = _statement;
	PREPARE dynquery FROM @statement;
	EXECUTE dynquery;
	DEALLOCATE PREPARE dynquery;
	
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_budget//
CREATE PROCEDURE budgeteer.bsp_get_budget(IN p_budgetID BIGINT )
BEGIN
	SELECT * FROM budgets WHERE budgets.budget_id = p_budgetID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_budget_count_by_subcategoryID//
CREATE PROCEDURE budgeteer.bsp_get_budget_count_by_subcategoryID(IN p_subcategoryID BIGINT )
BEGIN
	SELECT count(*) FROM budgets WHERE budgets.subcategory_ID = p_subcategoryID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_current_budget//
CREATE PROCEDURE budgeteer.bsp_get_current_budget(IN p_subcategoryID BIGINT)
BEGIN
	SELECT * FROM budgets WHERE budgets.subcategory_id = p_subcategoryID AND ((MONTH(budgets.start_date) = MONTH(curdate())) AND (YEAR(budgets.start_date) = YEAR(curdate())));
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_add_budget//
CREATE PROCEDURE budgeteer.bsp_add_budget(IN p_date DATE, IN p_subcategoryID BIGINT, IN p_amount DECIMAL(15,2), IN p_userID VARCHAR(30))
BEGIN
	INSERT INTO budgets (start_date, subcategory_ID, amount, user_ID) VALUES (p_date, p_subcategoryID, p_amount, p_userID);
	SELECT last_insert_id();
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_update_budget//
CREATE PROCEDURE budgeteer.bsp_update_budget(IN p_budgetID BIGINT, IN p_date DATE, IN p_subcategoryID BIGINT, IN p_amount DECIMAL(15,2))
BEGIN
	UPDATE budgets SET budgets.start_date = p_date, budgets.amount = p_amount, budgets.subcategory_ID = p_subcategoryID WHERE budgets.budget_id = p_budgetID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_delete_budget//
CREATE PROCEDURE budgeteer.bsp_delete_budget(IN p_budgetID BIGINT)
BEGIN
	DELETE FROM budgets WHERE budgets.budget_id = p_budgetID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_dollars_utilized//
CREATE PROCEDURE budgeteer.bsp_get_dollars_utilized(IN p_userID VARCHAR(30), IN p_date DATE, IN p_subcategoryType VARCHAR(10))
BEGIN
DECLARE _statement VARCHAR(1200);

	SET _statement = CONCAT("SELECT budgets.start_date, categories.category_id, budgets.subcategory_id, SUM(transactions.amount) as 'Amount', IF(subcategories.sinking_fund = 1, COALESCE((Select subcategories.balance - sum(vw.budgeted) - sum(vw.utilized) + budgets.amount From vw_budget_vs_utilized vw WHERE vw.start_date >= budgets.start_date AND vw.subcategory_id = budgets.subcategory_id GROUP BY vw.subcategory_id), 0.00), budgets.amount) as 'budget'");
	SET _statement = CONCAT(_statement, " FROM budgets LEFT JOIN transactions ON budgets.subcategory_id = transactions.subcategory_id and YEAR(budgets.start_date) = YEAR(transactions.date) and MONTH(budgets.start_date) = MONTH(transactions.date), subcategories, categories");
	SET _statement = CONCAT(_statement, " WHERE budgets.subcategory_id = subcategories.subcategory_id and subcategories.category_id = categories.category_id and budgets.user_id = '", p_userID, "'");
	
	IF (p_date IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, " AND YEAR(budgets.start_date) = YEAR('", p_date, "') AND MONTH(budgets.start_date) = MONTH('", p_date, "')");
	END IF;
	
	IF (p_subcategoryType IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, " AND subcategories.type = '", p_subcategoryType, "'");
	END IF;
	
	SET _statement = CONCAT(_statement, ' GROUP BY budgets.start_date, categories.category_id, budgets.subcategory_id ORDER BY budgets.start_date DESC, categories.description ASC, subcategories.description ASC');
	
	SET @statement = _statement;
	PREPARE dynquery FROM @statement;
	EXECUTE dynquery;
	DEALLOCATE PREPARE dynquery;
	
END// 

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_dollars_utilized_by_subcategoryID//
CREATE PROCEDURE budgeteer.bsp_get_dollars_utilized_by_subcategoryID(IN p_userID VARCHAR(30), IN p_date DATE, IN p_subcategoryID BIGINT)
BEGIN
DECLARE _statement VARCHAR(1200);

	SET _statement = CONCAT("SELECT budgets.start_date, categories.category_id, budgets.subcategory_id, SUM(transactions.amount) as 'Amount', IF(subcategories.sinking_fund = 1, COALESCE((Select subcategories.balance - sum(vw.budgeted) - sum(vw.utilized) + budgets.amount From vw_budget_vs_utilized vw WHERE vw.start_date >= budgets.start_date AND vw.subcategory_id = budgets.subcategory_id GROUP BY vw.subcategory_id), 0.00), budgets.amount) as 'budget'");
	SET _statement = CONCAT(_statement, " FROM budgets LEFT JOIN transactions ON budgets.subcategory_id = transactions.subcategory_id and YEAR(budgets.start_date) = YEAR(transactions.date) and MONTH(budgets.start_date) = MONTH(transactions.date), subcategories, categories");
	SET _statement = CONCAT(_statement, " WHERE budgets.subcategory_id = subcategories.subcategory_id and subcategories.category_id = categories.category_id and budgets.subcategory_id = ", p_subcategoryID, " and budgets.user_id = '", p_userID, "'");
	
	IF (p_date IS NOT NULL) THEN
		SET _statement = CONCAT(_statement, " AND budgets.start_date = '", p_date, "'");
	END IF;
	
	SET _statement = CONCAT(_statement, ' GROUP BY budgets.start_date, categories.category_id, budgets.subcategory_id ORDER BY budgets.start_date DESC, categories.description ASC, subcategories.description ASC');
	
	SET @statement = _statement;
	PREPARE dynquery FROM @statement;
	EXECUTE dynquery;
	DEALLOCATE PREPARE dynquery;
	
END// 

##### APPLICATION SETTINGS PROCEDURES #####
DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_application_settings//
CREATE PROCEDURE budgeteer.bsp_get_application_settings(IN p_userID VARCHAR(30))
BEGIN
	SELECT * FROM application_settings WHERE application_settings.user_id = p_userID;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_get_application_setting//
CREATE PROCEDURE budgeteer.bsp_get_application_setting(IN p_setting VARCHAR(30), IN p_userID VARCHAR(30))
BEGIN
	SELECT application_settings.value FROM application_settings WHERE application_settings.user_id = p_userID and application_settings.setting = p_setting;
END//

DROP PROCEDURE IF EXISTS Budgeteer.bsp_set_application_setting//
CREATE PROCEDURE budgeteer.bsp_set_application_setting(IN p_setting VARCHAR(30), IN p_value VARCHAR(30), IN p_userID VARCHAR(30))
BEGIN
	DELETE FROM application_settings WHERE application_settings.setting = p_setting AND application_settings.user_id = p_userID;
	INSERT INTO application_settings (setting, value, user_id) VALUES (p_setting, p_value, p_userID);
END//

##### SYSTEM PROCEDURES #####
DROP PROCEDURE IF EXISTS Budgeteer.bsp_delete_user//
CREATE PROCEDURE budgeteer.bsp_delete_user(IN p_userID VARCHAR(30))
BEGIN
	DELETE FROM transactions WHERE user_id = p_userID;
	DELETE FROM budgets WHERE user_id = p_userID;
	DELETE FROM accounts WHERE user_id = p_userID;
	DELETE FROM subcategories WHERE user_id = p_userID;
	DELETE FROM categories WHERE user_id = p_userID;
	DELETE FROM users WHERE user_id = p_userID;
END// 

delimiter ;

##### CREATE USER AND SET DATABASE SECURITY #####
#Only Grant Priviledges To Execute Procedures In Budgeteer Database
GRANT EXECUTE ON budgeteer.* TO budgeteeruser@localhost IDENTIFIED BY 'bgtrPwd!';
