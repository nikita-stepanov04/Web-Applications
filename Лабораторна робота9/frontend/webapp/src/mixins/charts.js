import Chart from "chart.js/auto";

export default {
    methods: {
        createCharts(dishPopularity, dishTypesPopularity) {
            const dishPopularityChart = this.$refs.dishPopularity;
            new Chart(dishPopularityChart, {
                type: 'bar',
                data: {
                    labels: dishPopularity.map(d => d.entityName),
                    datasets: [{
                        label: 'Dishes popularity',
                        data: dishPopularity.map(d => d.popularity),
                        borderWidth: 2
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false, // Отключение поддержки соотношения сторон
                    scales: {
                        x: {
                            beginAtZero: true,
                        },
                        y: {
                            beginAtZero: true,
                        },
                    }
                }
            });

            const dishTypesPopularityChart = this.$refs.dishTypesPopularity;
            new Chart(dishTypesPopularityChart, {
                type: 'bar',
                data: {
                    labels: dishTypesPopularity.map(d => d.entityName),
                    datasets: [{
                        label: 'Dish types popularity',
                        data: dishTypesPopularity.map(d => d.popularity),
                        borderWidth: 2
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false, // Отключение поддержки соотношения сторон
                    scales: {
                        x: {
                            beginAtZero: true,
                        },
                        y: {
                            beginAtZero: true,
                        },
                    }
                }
            });
        }
    }
}