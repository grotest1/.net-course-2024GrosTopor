
create table clients
(
	id uuid not null
		constraint clients_pk
			primary key,
	first_name varchar(150) not null,
	last_name varchar(150) not null,
	age smallint not null,
	phone varchar(15) not null,
	email varchar(15) not null
);


create table employees
(
	id uuid not null
		constraint employees_pk
			primary key,
	first_name varchar(150) not null,
	last_name varchar(150) not null,
	age smallint not null,
	phone varchar(15) not null,
	email varchar(15) not null
);


create table currencies
(
    id uuid not null constraint currency_pk primary key,
    name varchar(50) not null unique,
    code smallint not null unique
);


create table accounts
(
    id uuid not null constraint account_pk  primary key ,
    amount numeric(10, 2) not null,
    client_id uuid
		constraint accounts_clients_id_fk
			references clients,
    currency_id uuid
        constraint accounts_currencies_id_fk
            references currencies
);


insert into currencies(id, name, code)
values
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c001', 'USD', 840),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c002', 'EUR', 978),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c003', 'RUB', 643);


insert into employees(id, first_name, last_name, age, phone, email)
values
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c101', 'Дмитрий', 'Петров', 20, '789789787', 'fdsafsd@mail.ru'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c102', 'Сергей', 'Сидоров', 30, '778552244', ''),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c103', 'Олег', 'Пупкин', 40, '', '');


insert into clients(id, first_name, last_name, age, phone, email)
values
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c201', 'Лёня', 'Петров', 20, '45454578', 'fdsafs@ffff.ru'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c202', 'Георг', 'Сидоров', 30, '', ''),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c203', 'Димооон', 'Пупкин', 40, '', '');


insert into accounts(id, amount, client_id, currency_id)
values
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c301', 21.55, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c203', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c001'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c302', -35.80, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c202', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c001'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c303', 29.00, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c202', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c002'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c304', 99.99, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c203', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c002'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c305', 40.05, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c203', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c003');


/* провести выборки клиентов, у которых сумма на счету ниже
 определенного значения, отсортированных в порядке возрастания суммы */
select
       accounts.client_id,
       accounts.amount
from accounts
where accounts.amount < 31
order by accounts.amount;


/* провести поиск клиента с минимальной суммой на счете
    Вариант 1 - "если речь про просто получить мин сумму на счету" */
select
    MIN(accounts.amount) as min_amount
from accounts;


/* провести поиск клиента с минимальной суммой на счете 
   Вариант 2 - "если все-таки речь идет по поиске ИД клиентОВ с мин суммой и таких может быть несколько" */
select
    accounts.client_id,
    accounts.amount
from
accounts
    inner join
        (select
            MIN(accounts.amount) as min_amount
        from accounts) as accounts_min_amount
        on accounts.amount = accounts_min_amount.min_amount;


/*  провести подсчет суммы денег у всех клиентов банка */
select
     SUM(accounts.amount) as account_sum
from accounts;


/*  с помощью оператора Join, получить выборку банковских счетов и их владельцев */
select *
from clients
    inner join accounts as acc
        on clients.id = acc.client_id;


/* вывести клиентов от самых старших к самым младшим */
select *
from clients
order by clients.age desc;


/* подсчитать количество человек, у которых одинаковый возраст */
select
    clients.age,
    COUNT(clients.id)
from clients
group by clients.age
having COUNT(clients.id) > 1;


/*  сгруппировать клиентов банка по возрасту */
select
    clients.age,
    COUNT(clients.id)
from clients
group by clients.age;


/*  вывести только N человек из таблицы */
select * from employees limit 2;