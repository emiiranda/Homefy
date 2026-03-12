# Stage 1: Build React Application

FROM node:22-alpine AS build

WORKDIR /app

COPY package*.json ./
COPY vite.config.ts ./
COPY tsconfig*.json ./
COPY index.html ./
COPY src ./src
COPY public ./public

RUN npm install
RUN npm run build

# Stage 2: Nginx server files

FROM nginx:1.27-alpine

# Remove config padrão
RUN rm /etc/nginx/conf.d/default.conf

# Copia nossa configuração
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copia o build do Vite
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
