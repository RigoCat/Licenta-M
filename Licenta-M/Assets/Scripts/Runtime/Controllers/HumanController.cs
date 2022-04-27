using UnityEngine;

namespace MF
{
	[RequireComponent(typeof(HumanConfigurator))]
	public class HumanController : CustomActorController<HumanModel, HumanView, Movement, NervousSystem>
	{
		private const float DECREASE_TIME = 1f;

		private float lastTime;

		public override void Start()
		{
			lastTime = Time.time;
		}

		private void Update()
		{
			if (Time.time - lastTime > DECREASE_TIME)
			{
				lastTime = Time.time;

				Model.Health.Value = Model.Health.Value < 1 ? 0 : DecreseHealth(Model.Health.Value);
				Model.Money.Value = Model.Money.Value < 1 ? 0 : DecreseMoney(Model.Money.Value);
				Model.Energy.Value = Model.Energy.Value < 1 ? 0 : DecreseEnergy(Model.Energy.Value);
				Model.Hunger.Value = Model.Hunger.Value < 1 ? 0 : DecreseHunger(Model.Hunger.Value);
			}
		}

		public float DecreseEnergy(float modelEnergyValue)
		{
			var decreseEnergyValue = 1f;

			switch(Model.CurrentActivity)
			{
				case Activities.Working:
					decreseEnergyValue *= 3f;	// because human spends more time for doing work
					break;
				case Activities.Eating:
					decreseEnergyValue *= 0.5f;      // because human eats
					break;
				case Activities.Slepping:
					decreseEnergyValue *= 0f;      // because human does`t lose energy when he sleeps
					break;
				case Activities.Moving:
					decreseEnergyValue *= 2f;      // because human moves
					break;
				default:
					decreseEnergyValue *= 1f;
					break;
			}

			modelEnergyValue = modelEnergyValue - decreseEnergyValue;
			return modelEnergyValue;
		}

		public float DecreseHunger(float modelHungerValue)
		{
			var decreseHungerValue = 1f;

			switch (Model.CurrentActivity)
			{
				case Activities.Working:
					decreseHungerValue *= 4f;      // because human spends more time for doing something and lose more calories
					break;
				case Activities.Eating:
					decreseHungerValue *= 0f;      // because human eats
					break;
				case Activities.Slepping:
					decreseHungerValue *= 0.5f;      // because human sleeps and he consumes some calories
					break;
				case Activities.Moving:
					decreseHungerValue *= 1.5f;      // because human moves  he consume calories
					break;
				default:
					decreseHungerValue *= 1f;
					break;
			}

			modelHungerValue = modelHungerValue - decreseHungerValue;
			return modelHungerValue;
		}

		public float DecreseMoney(float modelMoneyValue)
		{
			var decreseMoneyValue = 1f;

			switch (Model.CurrentActivity)
			{
				case Activities.Working:
					decreseMoneyValue *= 0f;      // because human earns money, doesn`t lose
					break;
				case Activities.Eating:
					decreseMoneyValue *= 5f;      // because human eats
					break;
				case Activities.Slepping:
					decreseMoneyValue *= 0f;      // because human sleeps
					break;
				case Activities.Moving:
					decreseMoneyValue *= 0.5f;      // because human moves 
					break;
				default:
					decreseMoneyValue *= 1f;
					break;
			}

			modelMoneyValue = modelMoneyValue - decreseMoneyValue;
			return modelMoneyValue;
		}

		public float DecreseHealth(float modelHealthValue)
		{
			var decreseHealthValue = 0f;

			modelHealthValue = modelHealthValue - decreseHealthValue;
			return modelHealthValue;
		}
	}
}
