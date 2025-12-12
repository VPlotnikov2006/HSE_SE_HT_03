CREATE USER postgres WITH PASSWORD 'postgres';

CREATE DATABASE filestorage;
CREATE DATABASE fileanalysis;

GRANT ALL PRIVILEGES ON DATABASE filestorage TO postgres;
GRANT ALL PRIVILEGES ON DATABASE fileanalysis TO postgres;
