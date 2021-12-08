class WeatherForecastForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { weatherForecast: props.weatherForecast }
    }

    render() {
        return (
            <div>
                <details>
                    <summary>API Name: {this.state.weatherForecast.sourceApiName}</summary>
                    <p>{this.state.weatherForecast.temperature}</p>

                </details>
            </div>
        )
    }
}

class WeatherForecastSearchForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { city: "", weatherForecasts: [] };
        this.onCityChanged = this.onCityChanged.bind(this);
        this.onGetForecastClick = this.onGetForecastClick.bind(this);
    }

    onCityChanged(e) {
        this.setState({ city: e.target.value })
    }

    onGetForecastClick(e) {
        console.log(this.state.city);

        var xhr = new XMLHttpRequest();
        xhr.open("get", `/Weather/GetForecastsByCityName?city=${this.state.city}`, true)

        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            console.log(data);
            this.setState({ weatherForecasts: data })
        }.bind(this);

        xhr.send(null);
    }

    render() {
        return (
            <div>
                <input type="text"
                    value={this.state.city}
                    onChange={this.onCityChanged} />
                <p>
                    <button onClick={this.onGetForecastClick}>See forecast</button>
                </p>
                <div>
                    {
                        this.state.weatherForecasts.map(function (weatherForecast) {
                            return <WeatherForecastForm key={weatherForecast.id} weatherForecast={weatherForecast} />
                        })
                    }
                </div>
            </div>
        )
    }
}

ReactDOM.render(
    <WeatherForecastSearchForm />,
    document.getElementById("content")
);