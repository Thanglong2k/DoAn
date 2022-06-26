var APIConfig = {
    development: 'https://localhost:44361/api',
    production: 'local:8080'
}

export default APIConfig[process.env.NODE_ENV]; 