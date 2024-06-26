FROM node:lts-alpine
ENV NODE_ENV=production
WORKDIR /ui

COPY UI/public/ /ui/public
COPY UI/src/ /ui/src
COPY UI/package.json /ui/

RUN npm install --production --silent && mv node_modules ../
COPY UI/ .
EXPOSE 3000
RUN chown -R node /ui
USER node
CMD ["npm", "start"]
