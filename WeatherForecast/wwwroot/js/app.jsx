import WeatherItem from '../js/components/WeatherItem.jsx';

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
        var xhr = new XMLHttpRequest();
        xhr.open("get", `/Weather/GetWeatherByCityName?city=${this.state.city}`, true)

        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            console.log(data);
            this.setState({ weatherForecasts: data })
        }.bind(this);

        xhr.send(null);
    }

    render() {
        return (
            <div className="weatherForm">
                <input type="text"
                    value={this.state.city}
                    onChange={this.onCityChanged} />
                <p>
                    <button onClick={this.onGetForecastClick}>See forecast</button>
                </p>
                <div>
                    {
                        this.state.weatherForecasts.map(function (weatherForecast) {
                            return <WeatherItem key={weatherForecast.id} weatherForecast={weatherForecast} />
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