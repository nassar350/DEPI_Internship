const adviceId = document.getElementById("adviceId");
const advice = document.getElementById("advice");
const adviceBtn = document.getElementById("adviceBtn");

async function getAdvice() {
    try{
        const response = await fetch ("https://api.adviceslip.com/advice");
        const data = await response.json();
        adviceId.innerText = `Advice #${data.slip.id}`;
        advice.innerText = `"${data.slip.advice}"`;
    }
    catch (error)
    {
        advice.innerText = "Oops! Something Went Wrong.";
    }
}

adviceBtn.addEventListener("click", getAdvice);