begin tran
declare
	@maxRow int,
	@i int = 0
select ROW_NUMBER() OVER (order by OrderId asc) as RowNumber,* into #tempTable from Sales.SalesOrders

select @maxRow = max(RowNumber) from #tempTable

while(@i <= @maxRow)
	begin
		declare 
			@salesOrderId int,
			@orderValue decimal(18,2)

		if((select value from #tempTable where RowNumber = @i) = 0)
			begin
				select @salesOrderId = OrderId from #tempTable where RowNumber = @i
				select @orderValue = SUM(Value) from Sales.Items where OrderId = @salesOrderId

				update Sales.SalesOrders set value = @orderValue where OrderId = @salesOrderId
			end;
		set @i = @i + 1
	end;

drop table #tempTable 
commit