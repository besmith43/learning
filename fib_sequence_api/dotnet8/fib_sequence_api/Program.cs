var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/CalcFib/{fibnumber:int}", (int fibnumber) =>
{
	return calculateFib(fibnumber);
})
.WithName("CalcFib")
.WithOpenApi();

app.Run();


int calculateFib(int num)
{
	if (num == 1)
	{
		return 0;
	}

	if (num == 2)
	{
		return 1;
	}

	var x = 1;
	var y = 1;
	var sum = 0;

	for (int i = 2; i < num; i++)
	{
		sum = x + y;
		x = y;
		y = sum;	
	}

	return sum;

}

