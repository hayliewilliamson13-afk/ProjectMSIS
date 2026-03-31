pipeline {
    agent any

    environment {
        IMAGE_NAME = "haywilliamson/ProjectMSIS"
        IMAGE_TAG  = "${BUILD_NUMBER}"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    docker.build("${IMAGE_NAME}:${IMAGE_TAG}")
                }
            }
        }

        stage('Run Tests') {
            steps {
                script {
                    docker.image("${IMAGE_NAME}:${IMAGE_TAG}").inside {
                        sh 'npm test'   // or: pytest, mvn test, etc.
                    }
                }
            }
        }

        stage('Push to Docker Hub') {
            steps {
                script {
                    docker.withRegistry('https://index.docker.io/v1/', 'dockerhub-credentials') {
                        docker.image("${IMAGE_NAME}:${IMAGE_TAG}").push()
                        docker.image("${IMAGE_NAME}:${IMAGE_TAG}").push('latest')
                    }
                }
            }
        }

        stage('Deploy') {
            steps {
                sh """
                    docker stop your-app || true
                    docker rm your-app   || true
                    docker pull ${IMAGE_NAME}:${IMAGE_TAG}
                    docker run -d --name your-app -p 3000:3000 ${IMAGE_NAME}:${IMAGE_TAG}
                """
            }
        }
    }

    post {
        success { echo '✅ Pipeline succeeded!' }
        failure { echo '❌ Pipeline failed.' }
    }
}
