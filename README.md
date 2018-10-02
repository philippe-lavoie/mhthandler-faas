# mhthandler-faas
An OpenFaaS function to extract the first HTML element inside an MHT document.

This is an [http://www.openfaas.com](OpenFaas) function extract the first HTML element inside an MHT document. This project is deployed inside [https://hub.docker.com/r/philippelavoie/mhthandler-faas/](docker hub).

To use this inside OpenFaas, follow the normal flow:
- Login

  ```shell
  export GW_PASS=YourSecretPassword
  echo -n $GW_PASS | faas-cli login --username=admin --password-stdin
  ```
  
- Deploy. In this case, I'm deploying to a local kubernetes instance created with Docker.

  ```shell
  faas-cli deploy --image philippelavoie/mhthandler-faas --name athena -g http://localhost:31112
  ```

- Test. In this particular case, I'm sending the MTH file multipartDocument.mht to the athena function and sending the result to result.html.

  ```shell
  cat multipartDocument.mht | faas-cli invoke mhthandler > result.html
  ```


