create table Invitation (
	invitation_id uniqueidentifier not null,
	learning_id uniqueidentifier not null,
	sender_email varchar(255),
	sender_name varchar(255)
)

create table Learning (
	learning_id uniqueidentifier not null,
	title varchar(255),
	description text,
)

create table Registration (
	registration_id uniqueidentifier not null,
	learning_id uniqueidentifier not null,
	registration_email varchar(255) not null,
	last_activity_time datetime not null default getdate()
)