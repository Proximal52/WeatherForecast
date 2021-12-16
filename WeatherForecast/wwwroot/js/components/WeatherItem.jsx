class WeatherItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = { weatherForecast: props.weatherForecast }
    }

    render() {
        return (
            <div>
                <details>
                    <summary>API Name: {this.state.weatherForecast.sourceAPIName}</summary>
                    <ul className="list-group">
                        <li className="list-group-item" >Temperature: {Math.round(this.state.weatherForecast.temperature)}</li>
                        <li className="list-group-item" >Pressure: {Math.round(this.state.weatherForecast.pressure)}</li>
                        <li className="list-group-item" >Humidity: {Math.round(this.state.weatherForecast.humidity)}</li>
                    </ul>
                </details>
            </div>
        )
    }
}

export default WeatherItem;