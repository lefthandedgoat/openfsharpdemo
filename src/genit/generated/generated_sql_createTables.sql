

CREATE TABLE best_online_store.users(
  user_id                   SERIAL                    PRIMARY KEY NOT NULL,
  first_name                varchar(128)              NOT NULL,
  last_name                 varchar(128)              NOT NULL,
  email                     varchar(128)              NOT NULL,
  password                  varchar(60)               NOT NULL
);
  

CREATE TABLE best_online_store.products(
  product_id                SERIAL                    PRIMARY KEY NOT NULL,
  name                      varchar(1024)             NOT NULL,
  description               varchar(1024)             NOT NULL,
  price                     decimal(12, 2)            NOT NULL,
  category                  varchar(1024)             NOT NULL
);
  

CREATE TABLE best_online_store.carts(
  cart_id                   SERIAL                    PRIMARY KEY NOT NULL,
  register_fk               bigint                    REFERENCES best_online_store.users (user_id)
);
  

CREATE TABLE best_online_store.cartitems(
  cartitem_id               SERIAL                    PRIMARY KEY NOT NULL,
  cart_fk                   bigint                    REFERENCES best_online_store.carts (cart_id),
  product_fk                bigint                    REFERENCES best_online_store.products (product_id)
);
  

CREATE TABLE best_online_store.checkouts(
  checkout_id               SERIAL                    PRIMARY KEY NOT NULL,
  cart_fk                   bigint                    REFERENCES best_online_store.carts (cart_id)
);
  


GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA best_online_store TO best_online_store;
GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA best_online_store TO best_online_store;
  
  