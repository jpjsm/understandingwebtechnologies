create sequence if not exists Collection_Items_seq
increment by 1
start with 4
 nomaxvalue
nocycle
 nocache;
 
create sequence if not exists Images_seq
increment by 1
start with 1
 nomaxvalue
nocycle
 nocache;

create table if not exists COLLECTION_ITEMS (
        id bigint not null,
        name varchar2(255),
		summary varchar2(1000),
		description CLOB,
 		country varchar2(2),
		year smallint,
		price decimal,
		small_image bigint,
		image bigint,
        primary key (id)
);

create table if not exists IMAGES (
        id bigint not null,
		content CLOB,
        primary key (id)
);
create table if not exists ITEMS_TOPICS (
		item_id bigint,
		topic varchar2(100),
		 primary key (item_id, topic)
);
