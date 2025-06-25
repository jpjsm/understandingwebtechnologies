import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
      const url = "http://40.64.66.25/weatherforecast";

      //console.log("About to retrieve weather forecast data...\n");
      //const response = await fetch(url, {
      //    mode: "cors",
      //    method: 'GET',
      //    headers: {
      //        Accept: 'application/json',
      //    }
      //});

      //console.log("Fetch response status...\n");
      //console.log(response.status)

      //console.log("About to write the response object...\n");
      //console.log(response.text());

      //console.log("About to parse the response object to JSON...\n");
      //const weatherforecastdata = response.json();

      //console.log("About to write the response object as JSON...\n");
      //console.log(weatherforecastdata);

      // const response = await fetch('weatherforecast');
      const response = await fetch(url);
      const weatherforecastdata = await response.json();
      //console.log("About to write the response object as JSON...\n");
      //console.log(weatherforecastdata)
      //const data = JSON.parse('[{"date":"2020-10-09T00:14:42.1826581+00:00","temperatureC":-6,"temperatureF":22,"summary":"Hot"},{"date":"2020-10-10T00:14:42.1844399+00:00","temperatureC":39,"temperatureF":102,"summary":"Freezing"},{"date":"2020-10-11T00:14:42.1844431+00:00","temperatureC":5,"temperatureF":40,"summary":"Mild"},{"date":"2020-10-12T00:14:42.1844435+00:00","temperatureC":25,"temperatureF":76,"summary":"Chilly"},{"date":"2020-10-13T00:14:42.1844437+00:00","temperatureC":50,"temperatureF":121,"summary":"Bracing"}]');
      this.setState({ forecasts: weatherforecastdata, loading: false });
  }
}
