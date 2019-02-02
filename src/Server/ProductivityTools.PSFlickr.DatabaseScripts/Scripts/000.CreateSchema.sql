IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'flickr')
                            BEGIN
                            EXEC('CREATE SCHEMA flickr')
                            END