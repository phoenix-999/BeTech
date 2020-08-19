create or replace trigger TR_Table__AU on Products after update as
                    if exists(
                      select 1 
                      from inserted i
                      join deleted d on d.ProductId=i.ProductId -- по первичному ключу Table
                      where i.Barcode != isnull(d.Barcode,0)
                      )
                rollback tran;
                GO