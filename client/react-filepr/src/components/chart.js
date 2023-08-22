import { Bar, Chart } from "react-chartjs-2";
import { Chart as ChartJS } from 'chart.js/auto'

function App(props) {    
    const mylabels =[];
    const myDataValue = [];
    if (props.data.length > 0) {
        const data = props.data[0];
        for (let i = 0; i < data.countries.length; i++) {
            mylabels.push(data.countries[i].name);
            myDataValue.push(data.countries[i].value);

        };
    }
    return (
        <div className="App">
            <h1>Countries by values</h1>
            <div style={{ maxWidth: "650px" }}>
                <Bar

                    data={{
                        // Name of the variables on x-axies for each bar
                        labels: mylabels, //["1st bar", "2nd bar", "3rd bar", "4th bar"],
                        datasets: [
                            {
                                // Label for bars
                                label: "total count/value",
                                // Data or value of your each variable
                                data: myDataValue, // [1552, 1319, 613, 1400],
                                // Color of each bar
                                backgroundColor: ["aqua", "green", "red", "yellow"],
                                // Border color of each bar
                                borderColor: ["aqua", "green", "red", "yellow"],
                                borderWidth: 0.5,
                            },
                        ],
                    }}
                    // Height of graph
                    height={400}
                    options={{
                        maintainAspectRatio: false,
                        scales: {
                            yAxes: [
                                {
                                    ticks: {
                                        // The y-axis value will start from zero
                                        beginAtZero: true,
                                    },
                                },
                            ],
                        },
                        legend: {
                            labels: {
                                fontSize: 15,
                            },
                        },
                    }}
                />
            </div>
        </div>
    );
}

export default App;
