<?php
$url = 'http://localhost/api/users/Login/';
$ch = curl_init($url);
$data = array(
    'login' => $_POST["login"],
    'password' => $_POST["password"]
);
$jsonEncoded = json_encode($data);
curl_setopt($ch, CURLOPT_POSTFIELDS, $jsonEncoded);
echo($jsonEncoded);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
$result = curl_exec($ch);
//TODO zrobić coś z resultem // api link
curl_close($ch);
if($result==null)
    header('Location: '.$url);
?>
