using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDShared.Models;
using CRUDAPI.DataStore;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRUDAPI.Controllers;

[ApiController]
// [Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly DataStoreFactory _dataStoreFactory;

    public WeatherForecastController(DataStoreFactory dataStoreFactory)
    {
        _dataStoreFactory = dataStoreFactory;
    }


    [Route("WeatherForecast")]
	[HttpGet]
	[SwaggerOperation(Summary = "Get All Weather Forecasts", Description = "This endpoint returns all weather forecasts in the db table.", OperationId = "GetAllWeatherForecasts")]
	[ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IResult GetAllWeatherForecasts()
    {
        var result = _dataStoreFactory.GetAllWeatherForecasts();

        if (result is ErrorMessage)
        {
            return Results.Problem(
				new ProblemDetails
				{
					Status = (int)result.AsT1.StatusCode,
					Detail = result.AsT1.Message
				}
			);
        }

        if (result.AsT0.Count == 0)
        {
            return Results.NoContent();
        }

        return Results.Ok(result.AsT0);
    }

    [Route("WeatherForecast/{city}")]
	[HttpGet]
	[SwaggerOperation(Summary = "Get Weather Forecast By City", Description = "This endpoint returns the weather forecasts for the requested city from the db table if available.", OperationId = "GetWeatherForecastByCity")]
	[ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public IResult GetWeatherForecastByCity(
		[FromRoute, SwaggerParameter("City", Required = true)]
        string city
        )
    {
        var result = _dataStoreFactory.GetWeatherForecastByCity(city);

        if (result is ErrorMessage)
        {
            return Results.Problem(
                new ProblemDetails
                {
                    Status = (int)result.AsT1.StatusCode,
                    Detail = result.AsT1.Message
                }
            );
        }

        if (result.AsT0.Count == 0)
        {
            return Results.NoContent();
        }

        return Results.Ok(result.AsT0);
    }

    [Route("WeatherForecast")]
    [HttpPost]
    [SwaggerOperation(Summary = "Add Weather Forecast", Description = "This endpoint adds a weather forecast to the db table.", OperationId = "AddWeatherForecast")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IResult AddWeatherForecast(
        [FromBody, SwaggerParameter("Weather Forecast", Required = true)]
        WeatherForecast forecast
        )
    {
        var result = _dataStoreFactory.AddWeatherForecast(forecast);

        if (result is ErrorMessage)
        {
            return Results.Problem(
                new ProblemDetails
                {
                    Status = (int)result.AsT1.StatusCode,
                    Detail = result.AsT1.Message
                }
            );
        }

        return Results.Created();
    }

    [Route("WeatherForecast")]
    [HttpPut]
    [SwaggerOperation(Summary = "Update Weather Forecast", Description = "This endpoint updates a weather forecast in the db table.", OperationId = "UpdateWeatherForecast")]
    [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IResult UpdateWeatherForecast(
        [FromBody, SwaggerParameter("Weather Forecast", Required = true)]
        WeatherForecast forecast
        )
    {
        var result = _dataStoreFactory.UpdateWeatherForecast(forecast);

        if (result is ErrorMessage)
        {
            return Results.Problem(
                new ProblemDetails
                {
                    Status = (int)result.AsT1.StatusCode,
                    Detail = result.AsT1.Message
                }
            );
        }

        return Results.Ok(result.AsT0);
    }

    [Route("WeatherForecast")]
    [HttpDelete]
    [SwaggerOperation(Summary = "Delete Weather Forecast", Description = "This endpoint deletes a weather forecast from the db table.", OperationId = "DeleteWeatherForecast")]
    [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public IResult DeleteWeatherForecast(
        [FromBody, SwaggerParameter("Weather Forecast", Required = true)]
        WeatherForecast forecast
        )
    {
        var result = _dataStoreFactory.DeleteWeatherForecast(forecast);

        if (result is ErrorMessage)
        {
            return Results.Problem(
                new ProblemDetails
                {
                    Status = (int)result.AsT1.StatusCode,
                    Detail = result.AsT1.Message
                }
            );
        }

        return Results.Ok(result.AsT0);
    }
}