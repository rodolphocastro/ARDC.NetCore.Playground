# NetCore Playground

## Status

### Master

[![Build Status](https://travis-ci.com/rodolphocastro/ARDC.NetCore.Playground.svg?branch=master)](https://travis-ci.com/rodolphocastro/ARDC.NetCore.Playground)

### Develop

[![Build Status](https://travis-ci.com/rodolphocastro/ARDC.NetCore.Playground.svg?branch=develop)](https://travis-ci.com/rodolphocastro/ARDC.NetCore.Playground)

## About this Repository

This repository aims to provide examples on how to build a RESTFul API using Microsoft's dotNet Core 2.2.

## Docker

This repository is available at [DockerHub](https://cloud.docker.com/repository/docker/rodolphoalves/ardcnetcoreplaygroundapi).

### Running the image

You can use the following command to run this API: `docker run -p 80:80 -it -v C:\dockervolumes\playground:/app/settings ardcnetcoreplaygroundapi`

Change `c:\dockervolumes\playground` to wherever you store you `appsettings.docker.json`. Failing to map the settings file will lead to an `ArgumentNullException`.

## Technologies

WIP

## Contributing

WIP