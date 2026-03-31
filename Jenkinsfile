pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git branch: 'master',
                    url: 'https://github.com/hayliewilliamson13-afk/ProjectMSIS.git'
            }
        }

        stage('Build') {
            steps {
                echo 'Building...'
                // your build commands here
            }
        }

        stage('Test') {
            steps {
                echo 'Testing...'
                // your test commands here
            }
        }
    }
}
