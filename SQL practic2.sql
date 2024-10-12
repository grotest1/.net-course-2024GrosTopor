
create table clients
(
	id uuid not null
		constraint clients_pk
			primary key,
	name varchar(150) not null,
	age smallint not null
); 


create table employees
(
	id uuid not null
		constraint employees_pk
			primary key,
	name varchar(150) not null,
	age smallint not null
);


create table currencies
(
    id uuid not null constraint currency_pk primary key ,
    name varchar(50) not null ,
    code smallint
);


create table accounts
(
    id uuid not null constraint account_pk  primary key ,
    amount int not null,
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


insert into employees(id, name, age)
values
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c101', 'Петров', 20),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c102', 'Сидоров', 30),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c103', 'Пупкин', 40);


insert into clients(id, name, age)
values
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c201', 'Петрович', 20),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c202', 'Сидоровав', 30),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c203', 'Пупкинуев', 40);


insert into accounts(id, amount, client_id, currency_id)
values
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c301', 21, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c203', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c001'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c302', 35, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c202', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c001'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c303', 29, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c202', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c002'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c304', 99, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c203', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c002'),
('fe1a4ef2-87c1-4d20-b6bf-3de2e088c305', 40, 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c203', 'fe1a4ef2-87c1-4d20-b6bf-3de2e088c003');


/* провести выборки клиентов, у которых сумма на счету ниже
 определенного значения, отсортированных в порядке возрастания суммы */
select
       accounts.client_id,
       accounts.amount
from accounts
where accounts.amount < 31
order by accounts.amount;


/* провести поиск клиента с минимальной суммой на счете */
select
       accounts.client_id,
       accounts.amount
from accounts
order by accounts.amount
limit 1;


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
select SUM(inn.count_clients)
from (
    select COUNT(clients.id) as count_clients,
           clients.age
    from clients
    group by clients.age
    having COUNT(clients.id) > 1
) as inn;


/*  сгруппировать клиентов банка по возрасту */
select clients.name,
       clients.age
from clients
order by clients.age;


/*  вывести только N человек из таблицы */
select * from accounts limit 2;