delete from IMAGES;
insert into IMAGES(id, content) values(1, 'MTIzNDU=');
insert into IMAGES(id, content) values(2, 'MTIzNDU=');
insert into IMAGES(id, content) values(3, 'TEST');
insert into IMAGES(id, content) values(4, 'TEST4');
insert into IMAGES(id, content) values(5, 'TEST5');
insert into IMAGES(id, content) values(6, 'TEST6');

delete from COLLECTION_ITEMS;
insert into COLLECTION_ITEMS (id, name, summary, description, year, country, price, small_image, image) values(1, 'The Penny Black', 'The very first stamp', 'The very first post stamp but suprisely not the most expensive one', 1840, 'uk', 1000, 3, 4);
insert into COLLECTION_ITEMS (id, name, summary, description, year, country, price, small_image, image) values(2, 'Juke', 'The Juke stature', 'Porcelain stature of Juke', 1996, 'us', 1000000, 1, 2);
insert into COLLECTION_ITEMS (id, name, summary, description, year, country, price, small_image, image) values(3, 'Juggling Juke', 'The Juggling Juke painting', 'Post modernistic oil painting of the juggling Juke', 2000, 'us', 2000000, 5, 6);
--insert into COLLECTION_ITEMS (id, name, summary, description, year, country, price, small_image, image) values(4, 'Post Office Mauritius', 'Two penny blue Mauritius post stamp', 'A very very expensive stamp', 1847, 'mu', 1000000, 3, 4);

delete from ITEMS_TOPICS;
insert into ITEMS_TOPICS (item_id, topic) values(1, 'History');
insert into ITEMS_TOPICS (item_id, topic) values(2, 'Arts');
insert into ITEMS_TOPICS (item_id, topic) values(2, 'Programming');
insert into ITEMS_TOPICS (item_id, topic) values(3, 'Arts');
insert into ITEMS_TOPICS (item_id, topic) values(3, 'Programming');

