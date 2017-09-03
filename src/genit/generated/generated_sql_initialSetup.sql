
DROP OWNED BY best_online_store;
DROP USER IF EXISTS best_online_store;

DROP SCHEMA IF EXISTS best_online_store;
CREATE SCHEMA best_online_store;

CREATE USER best_online_store WITH ENCRYPTED PASSWORD 'NOTSecure1234';
GRANT USAGE ON SCHEMA best_online_store to best_online_store;
ALTER DEFAULT PRIVILEGES IN SCHEMA best_online_store GRANT SELECT ON TABLES TO best_online_store;
GRANT CONNECT ON DATABASE "best_online_store" to best_online_store;