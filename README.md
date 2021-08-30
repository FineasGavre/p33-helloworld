# Hello World
[![.NET](https://github.com/FineasGavre/p33-helloworld/actions/workflows/dotnet.yml/badge.svg)](https://github.com/FineasGavre/p33-helloworld/actions/workflows/dotnet.yml)

[View on Heroku](https://helloworldp33.herokuapp.com/)

This is a hello world project.

## Development
### Database Access
When the environment variable `ASPNETCORE_ENVIRONMENT` has the value `DEVELOPMENT`, the application will use the ConnectionString `LocalPostgresContext` found in `appsettings.json`. Switching the value to `PRODUCTION` will make the application use a database supplied via the `DATABASE_URL` environment variable.
### Initial Administrator Creation
When the environment variable `DEFAULT_ADMIN_USER_CREDENTIALS` is set, the application will create an administrator account with the credentials taken from the environment variable value with format (`<email>;<password>`).

For example, setting the environment variable to:
```bash
DEFAULT_ADMIN_USER_CREDENTIALS=admin@acme.com;Password1234!
```
will create an account that has the email `admin@acme.com` and password `Password1234!`. This account will be added automatically to the `Administrator` role.

The account will **not be created** if a user with that email already exists, but it **will be assigned** to the `Administrator` role.

## Versioning
### On Manual Deploys
1. You can set the version of the app using the `VERSION` build argument in `docker build`.
```bash
docker run -e DATABASE_URL={database_url} -dp {external_port}:80 p33-helloworld --build-args=VERSION=1.0.0.1
```

### Github Actions CI/CD Flow
1. You can modify the version in the [`dotnet.yml`](https://github.com/FineasGavre/p33-helloworld/blob/master/.github/workflows/dotnet.yml) file.

## How to run / deploy
### Locally (via Docker)

1. Build the container image
```bash
docker build -t p33-helloworld .
```
2. Start the app container
```bash
docker run -e DATABASE_URL={database_url} -dp {external_port}:80 p33-helloworld
```
Replace ```database_url``` with a Postgres Database URI (starting with `postgres://`).  
Replace ```external_port``` with the desired port on the host machine.
    

### On Heroku
Ensure you have the Heroku CLI **installed** and **authenticated**.

1. Log into the Heroku Container Registry (you only need to run this once)
```bash
heroku container:login
```

2. Create an app in Heroku (skip this if you already have created an app)
```bash
heroku create
```

3. Build the container image and push it to the Heroku Registry
```bash
heroku container:push web
```

4. Provision a Postgres database and add required Environment Variables
```bash
heroku addons:create heroku-postgresql:hobby-dev
heroku config:set ASPNETCORE_ENVIRONMENT=Production
```

5. Release the image to the app
```bash
heroku container:release web
```

## Authors
Created by Fineas Gavre during the Principal33 Internship.

[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)

