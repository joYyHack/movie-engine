FROM node:lts-alpine
ENV NODE_ENV=production
WORKDIR /ui

COPY ui/public/ /ui/public
COPY ui/src/ /ui/src
COPY ui/package.json /ui/

RUN npm install --production --silent && mv node_modules ../
COPY ui/ .
EXPOSE 3000
RUN chown -R node /ui
USER node
CMD ["npm", "start"]
