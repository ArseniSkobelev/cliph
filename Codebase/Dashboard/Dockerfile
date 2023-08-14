FROM node:18-alpine AS builder

ARG SECRET_AUTH_API_URI
ARG SECRET_CSCA_HEADER_NAME
ARG SECRET_CSCA_TOKEN

ENV AUTH_API_URI=$SECRET_AUTH_API_URI
ENV CSCA_HEADER_NAME=$SECRET_CSCA_HEADER_NAME
ENV CSCA_TOKEN=$SECRET_CSCA_TOKEN
ENV ORIGIN=localhost:3000

RUN mkdir /app && mkdir /app/data

COPY . /app

RUN cd /app && npm install && \
    npm run build

FROM node:18-alpine

RUN mkdir /app

COPY --from=builder /app/build /app/build
COPY --from=builder /app/package*.json /app/

RUN cd /app && \
    npm ci --production

WORKDIR /app

CMD ["node", "build"]